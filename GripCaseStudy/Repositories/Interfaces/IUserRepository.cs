using GripCaseStudy.Models;

namespace GripCaseStudy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(string username, string password);
        Task<User> GetByName(string username);
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
