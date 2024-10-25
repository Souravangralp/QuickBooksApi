using Microsoft.AspNetCore.Mvc;
using QuickBookApi.Repository.Interfaces;

namespace QuickBookApi.Controllers
{
    [ApiController]
    public class QuickBooksController : ControllerBase
    {
        #region Fields

        private readonly IQuickBookService _quickBookService;

        #endregion

        #region Ctor

        public QuickBooksController(IQuickBookService quickBookService)
        {
            _quickBookService = quickBookService;
        }

        #endregion

        #region Methods

        [HttpPost("/quickBooks/login")]
        public async Task<IActionResult> GetAuthorizationCode()
        {
            return Ok(await _quickBookService.GetAuthUrl());
        }

        [HttpGet("/quickBooks/redirect-url")]
        public async Task<IActionResult> RedirectUrl(string code, string state)
        {
            return Ok(await _quickBookService.Redirect(state, code));
        }

        [HttpGet("/quickBooks/authcode")]
        public async Task<IActionResult> GetAuthCode()
        {
            return Ok(await _quickBookService.GetAuthCode());
        }

        [HttpGet("/quickBooks/getRefreshToken")]
        public async Task<IActionResult> GetRefreshToken(string refreshToken, string authCode)
        {
            return Ok(await _quickBookService.GetRefreshToken(refreshToken, authCode));
        }

        [HttpGet("/quickBooks/getCompanyInfo")]
        public async Task<IActionResult> GetCompanyInfo() 
        {
            return Ok(await _quickBookService.GetCompanyInfo());
        }

        #endregion
    }
}