using QuickBookApi.Models.Responses;

namespace QuickBookApi.Repository.Interfaces;

public interface IQuickBookService
{
    Task<string> GetAuthUrl();

    Task<bool> Redirect(string state, string code);

    Task<AccessTokenResponse> GetAuthCode();

    Task<AccessTokenResponse> GetRefreshToken(string refreshToken, string authCode);

    Task<CompanyInfo> GetCompanyInfo();
}
