using Newtonsoft.Json;
using QuickBookApi.Constants;
using QuickBookApi.Models.Responses;
using QuickBookApi.Repository.Interfaces;

namespace QuickBookApi.Repository.Services;

public class QuickBookService : IQuickBookService
{
    #region Fields

    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    #endregion

    #region Ctor

    public QuickBookService(IConfiguration configuration,
        HttpClient httpClient,
        ITokenService tokenService)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    #endregion

    #region Methods

    public async Task<string> GetAuthUrl()
    {
        var QuickBookConfig = _configuration.GetSection("QuickBooks");

        string clientId = QuickBookConfig.GetSection("CLIENT_ID").Value ?? "";
        string responseType = "code";
        string scope = string.Format(QuickBook.Scope.Accounting.scope);
        string redirectUri = QuickBookConfig.GetSection("REDIRECT_URL").Value ?? "";
        string state = QuickBookConfig.GetSection("STATE").Value ?? "";

        string authUrl = $"https://appcenter.intuit.com/connect/oauth2?client_id={clientId}&response_type={responseType}&scope={scope}&redirect_uri={Uri.EscapeDataString(redirectUri)}&state={state}";

        return await Task.FromResult(authUrl);
    }

    public async Task<bool> Redirect(string state, string code)
    {
        if (string.IsNullOrEmpty(code)) throw new Exception("Please provide state and code");

        var QuickBookConfig = _configuration.GetSection("QuickBooks");

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri", QuickBookConfig.GetSection("REDIRECT_URL").Value ?? ""),
            new KeyValuePair<string, string>("client_id", QuickBookConfig.GetSection("CLIENT_ID").Value ?? ""),
            new KeyValuePair<string, string>("client_secret", QuickBookConfig.GetSection("CLIENT_SECRET").Value ?? "")
        });

        var response = await _httpClient.PostAsync(QuickBook.Route.Token.route, requestData);

        if (!response.IsSuccessStatusCode) throw new Exception();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<AccessTokenResponse>(jsonResponse);

        result.realmId = code;

        return await _tokenService.Save(result);
    }

    public async Task<AccessTokenResponse> GetAuthCode() 
    {
        return await _tokenService.Get();
    }

    public async Task<AccessTokenResponse> GetRefreshToken(string refreshToken, string authCode)
    {
        var tokenUrl = QuickBook.Route.Token.route;
        var requestData = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", $"{refreshToken}" }
        };

        var content = new FormUrlEncodedContent(requestData);

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authCode}");

        var response = await _httpClient.PostAsync(tokenUrl, content);

        if (!response.IsSuccessStatusCode) throw new Exception("please check your token");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<AccessTokenResponse>(jsonResponse);

        return result ?? new();
    }

    public async Task<CompanyInfo> GetCompanyInfo()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://quickbooks.api.intuit.com/v3/company/9341453354019744/companyinfo/9341453354019744");
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "••••••");
        request.Headers.Add("Cookie", "AKES_GEO=IN~CH; _abck=3462C42BB107FEC1BA9938CB993E185F~-1~YAAQTgVaaOod7ZSSAQAAWhNAwgzQIYtT9MOeGD8a4U+3w/EPynxwqo44VdG2SnAhHSF0fciHR+bs5YJGey+v9ALCxca1YHDqsZYiqBo5LotZP3oLpD+NSGlFknQxrIJrhbBqjy6q+SLhSSFoukhepiLHjTQpuX7nb0OaPJRwHsWsXzIVzGsyA/Xen7LirvvJwC3rvILGEWdjjS1/bkcScrVuYL/EYup12X2yIc85aDpQGs5k0nlEWc/zDRr/TEKyy2+BNo0AipsWJC7aMvjigqRokAT44omiP9VsmXEgxFzNRvU1lsX1iz5Iiw+zlNrnWAGSIDvmDmxKxkYrGNHGn4L7UAmwOf5SMI5+D3KJBrKLCRdwSdRwMSClFkXMrk1bWSf6pYpQn2VOMCh6D11dUgU2E+FR/PE1iGE=~-1~-1~-1; ivid=a821f25b-e8aa-4627-949f-49a3581ecea5");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return new CompanyInfo();
    }

    #endregion
}
