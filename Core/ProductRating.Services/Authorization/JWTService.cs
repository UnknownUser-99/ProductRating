using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ProductRating.Contracts.Authorization;
using ProductRating.Data.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ProductRating.Services.Authorization
{
    public class JWTService : IJWTService
    {
        private readonly JWTServiceOptions _options;

        public JWTService(IOptions<JWTServiceOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(int id, TimeSpan time)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime currentDateTime = DateTime.UtcNow;

            DateTime expirationDateTime = currentDateTime.Add(time);

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(_options.Issuer, null, claims, expires: expirationDateTime, signingCredentials: credentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string token = tokenHandler.WriteToken(tokenDescriptor);

            return token;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.UTF8.GetBytes(_options.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _options.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return claimsPrincipal;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}