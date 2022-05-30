using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoMansSkyRecipies.CustomSettings;
using NoMansSkyRecipies.Helpers;
using UsersData;
using UsersData.Entities;

namespace NoMansSkyRecipies.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserDataDbContext _userContext;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserDataDbContext context, JwtSettings jwtSettings)
        {
            this._userContext = context;
            this._jwtSettings = jwtSettings;
        }

        public string GetAccessToken(string userName, string password)
        {
            string result = string.Empty;
            var passwordHash = password.GetHash();

            var user = this._userContext.Users
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .SingleOrDefault(x =>
                x.UserName == userName && x.PasswordHash == passwordHash);

            if (user == null)
            {
                return result;
            }

            user.Token = this.CreateAccessTokenForUser(user);

            return user.Token;
        }

        public int SetUserRefreshToken(int userId, string token, DateTime expiryTime)
        {
            var user = this._userContext.Users.SingleOrDefault(x =>
                x.Id == userId);

            if (user == null)
            {
                return 0;
            }

            user.RefreshToken = token;
            user.RefreshTokenExpiryDate = expiryTime;

            this._userContext.SaveChanges();

            return user.Id;
        }

        public string CreateAccessTokenForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(this._jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.Add(this._jwtSettings.ValidDuration),
                Issuer = this._jwtSettings.Issuer,
                Audience = this._jwtSettings.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, string.Join(',', user.UserRoles.Select(x => x.Role.RoleName)))
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public (string, TimeSpan) GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return (Convert.ToBase64String(randomNumber), _jwtSettings.RefreshTokenValidity);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = this._jwtSettings.ValidateAudience,
                ValidateIssuer = this._jwtSettings.ValidateIssuer,
                ValidateIssuerSigningKey = this._jwtSettings.ValidateIssuerSigningKey,
                ValidateLifetime = this._jwtSettings.ValidateLifetime,
                ValidIssuer = this._jwtSettings.Audience,
                ValidAudience = this._jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public User GetUserByName(string name)
        {
            return this._userContext.Users
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .SingleOrDefault(x => x.UserName == name);
        }

        public void UpdateUser(User user)
        {
            this._userContext.Users.Update(user);
            this._userContext.SaveChanges();
        }

        public void RevokeRefreshTokenForUSer(string userName)
        {
            var user = this.GetUserByName(userName);

            if (user != null)
            {

            }

        }
    }
}
