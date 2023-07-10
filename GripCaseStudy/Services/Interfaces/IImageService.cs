using System.IO;

namespace GripCaseStudy.Services.Interfaces
{
    public interface IImageService
    {
        public string GenerateThumbnailBase64(Stream stream);
    }
}
