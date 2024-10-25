using QuickBookApi.Models.Responses;

namespace QuickBookApi.Repository.Interfaces;

public interface ITokenService
{
    Task<AccessTokenResponse> Get();

    Task<bool> Save(AccessTokenResponse accessTokenResponse);
}
