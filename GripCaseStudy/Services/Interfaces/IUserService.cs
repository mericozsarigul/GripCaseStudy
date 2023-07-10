using GripCaseStudy.Models;

namespace GripCaseStudy.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> Authenticate(string username, string password);
        public Task<User?> GetUserFromToken(string token);

    }
}
