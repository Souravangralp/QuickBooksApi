namespace QuickBookApi.Models.Responses;

public class AccessTokenResponse
{
    public string token_type { get; set; }
    public int x_refresh_token_expires_in { get; set; }
    public string refresh_token { get; set; }
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string realmId { get; set; }
}
