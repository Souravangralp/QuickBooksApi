using Newtonsoft.Json;
using QuickBookApi.Models.Responses;
using QuickBookApi.Repository.Interfaces;

namespace QuickBookApi.Repository.Services;

public class TokenService : ITokenService
{
    //public const string Resources = @"wwwroot\resources";

    private readonly string _filePath = @"wwwroot\resources\token.json";

    public async Task<AccessTokenResponse> Get()
    {
        if (!File.Exists(_filePath)) return null;

        var json = await File.ReadAllTextAsync(_filePath);

        return JsonConvert.DeserializeObject<AccessTokenResponse>(json);
    }

    public async Task<bool> Save(AccessTokenResponse accessTokenResponse)
    {
        var json = JsonConvert.SerializeObject(accessTokenResponse, Formatting.Indented);

        await File.WriteAllTextAsync(_filePath, json);

        return true;
    }
}
