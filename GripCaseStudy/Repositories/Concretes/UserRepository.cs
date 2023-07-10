using Dapper;
using GripCaseStudy.Models;
using GripCaseStudy.Repositories.Interfaces;

namespace GripCaseStudy.Repositories.Concretes
{
    public class UserRepository : IUserRepository
    {
        private GripCaseContext _context;

        public UserRepository(GripCaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Users";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User> Get(string username, string password)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Users WHERE Username = @username and Password = @password";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username, password });
        }

        public async Task Create(User user)
        {
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task Update(User user)
        {
            using var connection = _context.CreateConnection();
            var sql = "UPDATE Users SET Username = @Username, Password = @Password WHERE Id = @Id";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Users WHERE Id = @id";
            await connection.ExecuteAsync(sql, new { id });
        }


        public async Task<User> GetByName(string username)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Users WHERE Username = @username";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });
        }
    }
}
