using System;
using System.Security.Claims;
using UsersData.Entities;

namespace NoMansSkyRecipies.Services
{
    public interface IAuthService
    {
        User GetUserByName(string name);

        string GetAccessToken(string userName, string password);

        int SetUserRefreshToken(int userId, string token, DateTime expiryTime);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        (string refreshToken, TimeSpan validityTimeSpan) GenerateRefreshToken();

        string CreateAccessTokenForUser(User user);
        void UpdateUser(User user);
        void RevokeRefreshTokenForUSer(string userName);
    }
}
