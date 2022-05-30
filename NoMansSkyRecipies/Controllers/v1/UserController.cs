using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoMansSkyRecipies.Models;
using NoMansSkyRecipies.Services;

namespace NoMansSkyRecipies.Controllers.v1
{
    [Route("api/v1/Auth")]
    [ApiController, Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            this._authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserModel user)
        {
            var token = this._authService.GetAccessToken(user.UserName, user.Password);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Incorrect user name and/or password");
            }

            var targetUser = this._authService.GetUserByName(user.UserName);

            targetUser.Token = token;
            var refresh = this._authService.GenerateRefreshToken();
            targetUser.RefreshTokenExpiryDate = DateTime.Now.Add(refresh.validityTimeSpan);
            targetUser.RefreshToken = refresh.refreshToken;

            this._authService.UpdateUser(targetUser);

            return Ok(new { AccessToken = token, RefreshToken = refresh.refreshToken});
        }
        
        [HttpPost]
        [Route("Refresh")]
        public IActionResult RefreshToken([FromBody, NotNull]RefreshTokenModel refreshModel)
        {
            var access = refreshModel.AccessToken;
            var refresh = refreshModel.RefreshToken;

            var principal = this._authService.GetPrincipalFromExpiredToken(access);
            var userName = principal.Identity.Name;

            var user = this._authService.GetUserByName(userName);

            if (user == null || user.RefreshToken != refresh || user.RefreshTokenExpiryDate <= DateTime.Now)
            {
                return BadRequest();
            }

            var newAccess = this._authService.CreateAccessTokenForUser(user);
            var newRefresh = this._authService.GenerateRefreshToken();

            user.RefreshToken = newRefresh.refreshToken;
            user.RefreshTokenExpiryDate = DateTime.Now.Add(newRefresh.validityTimeSpan);

            this._authService.UpdateUser(user);

            return new ObjectResult(new
            {
                accessToken = newAccess,
                refreshToken = newRefresh.refreshToken
            });
        }

        [HttpPost]
        [Route("Revoke")]
        public IActionResult RevokeToken([FromBody] RevokeTokenModel model)
        {
            var userName = User.Identity.Name;
            var user = this._authService.GetUserByName(userName);

            if (user == null) return BadRequest("User not found");

            this._authService.RevokeRefreshTokenForUSer(userName);

            return NoContent();
        }
    }
}
