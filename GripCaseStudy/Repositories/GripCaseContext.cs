using Dapper;
using GripCaseStudy.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GripCaseStudy.Repositories
{
    public class GripCaseContext
    {
        protected readonly IConfiguration Configuration;

        public GripCaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task Init()
        {
            using var connection = CreateConnection();
            await _initUsers();
            await _initImages();
            await _seedUser("admin","password");

            async Task _initUsers()
            {
                var sql = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,Username TEXT,Password TEXT);";
                await connection.ExecuteAsync(sql);
            }

            async Task _initImages()
            {
                var sql = "CREATE TABLE IF NOT EXISTS Images (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,UserId INTEGER NOT NULL, ImageBase64 TEXT, ImageThumbBase64 TEXT);";
                await connection.ExecuteAsync(sql);
            }

            async Task _seedUser(string username, string password)
            {
                var userSQL = "SELECT * FROM Users WHERE Id = 1";
                var user = await connection.QueryAsync<User>(userSQL);

                if(!user.Any())
                {
                    var sql = "INSERT INTO Users(Username, Password) VALUES(@username, @password)";
                    await connection.ExecuteAsync(sql, new { username, password });
                }
            }
        }
    }
}
