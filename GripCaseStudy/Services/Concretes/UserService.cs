using GripCaseStudy.Models;
using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GripCaseStudy.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        protected readonly IConfiguration Configuration;

        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            Configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<User?> Authenticate(string username, string password)
        {

            var user = await _userRepository.Get(username, password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> GetUserFromToken(string token)
        {
            token = token.Replace("Bearer ", "");
            var key = Configuration.GetValue<string>("Security:JwtKey")
                        ?? throw new Exception("Jwt key can not be null.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid");
                    if (userIdClaim != null)
                    {
                        var username = userIdClaim.Value;
                        if (username != null)
                        {
                            var user = await _userRepository.GetByName(username);
                            return user;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
