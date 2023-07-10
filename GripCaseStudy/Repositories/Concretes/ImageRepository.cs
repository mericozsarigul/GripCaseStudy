using Dapper;
using GripCaseStudy.Models;
using GripCaseStudy.Repositories.Interfaces;

namespace GripCaseStudy.Repositories.Concretes
{
    public class ImageRepository : IImageRepository
    {
        private GripCaseContext _context;

        public ImageRepository(GripCaseContext context)
        {
            _context = context;
        }

        public async Task Create(ImageModel image)
        {
            using var connection = _context.CreateConnection();
            var sql = "INSERT INTO Images (UserId, ImageBase64) VALUES (@UserId, @ImageBase64)";
            await connection.ExecuteAsync(sql, image);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ImageModel>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Images";
            return await connection.QueryAsync<ImageModel>(sql);
        }

        public async Task<ImageModel> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Images WHERE Id = @id";
            return await connection.QuerySingleOrDefaultAsync<ImageModel>(sql, new { id });
        }

        public async Task<IEnumerable<ImageModel>> GetUserAllImages(int userId)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Images WHERE UserId = @userId";
            return await connection.QueryAsync<ImageModel>(sql, new { userId });
        }

        public Task Update(ImageModel image)
        {
            throw new NotImplementedException();
        }
    }
}
