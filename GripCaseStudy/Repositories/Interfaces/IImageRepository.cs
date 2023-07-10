using GripCaseStudy.Models;

namespace GripCaseStudy.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageModel>> GetAll();
        Task<IEnumerable<ImageModel>> GetUserAllImages(int userId);
        Task<ImageModel> GetById(int id);
        Task Create(ImageModel image);
        Task Update(ImageModel image);
        Task Delete(int id);
    }
}
