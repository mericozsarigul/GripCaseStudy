using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Services.Interfaces;
using SixLabors.ImageSharp.Formats.Jpeg;
 

namespace GripCaseStudy.Services.Concretes
{
    public class ImageService : IImageService
    {
        protected readonly IConfiguration Configuration;

        public ImageService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateThumbnailBase64(Stream stream)
        {
            using (var image = Image.Load(stream))
            {
                var aspectDirection = Configuration.GetValue<string>("Image:ThumbnailAspectDirection")
                        ?? throw new Exception("Thumbnail aspect direction can not be null (height or width).");

                var aspectRatio = Configuration.GetValue<string>("Image:ThumbnailAspectRatio")
                        ?? throw new Exception("Thumbnail aspect ratio can not be null.");

                if(!int.TryParse(aspectRatio,out int aspect))
                {
                    throw new Exception("Thumbnail aspect ratio should be integer.");
                }

                int thumbnailHeight=0;
                int thumbnailWidth=0;

                if (aspectDirection == "height")
                {
                    thumbnailHeight = aspect;
                    thumbnailWidth = Convert.ToInt32(image.Width * thumbnailHeight / (double)image.Height);
                }
                else if (aspectDirection == "width")
                {
                    thumbnailWidth = aspect;
                    thumbnailHeight = Convert.ToInt32(image.Height * thumbnailWidth / (double)image.Width);
                }
                else
                {
                    throw new Exception("Thumbnail aspect direction can not be null (height or weight).");
                }

                image.Mutate(x => x.Resize(thumbnailWidth, thumbnailHeight));

                using var memoryStream = new MemoryStream();
                image.Save(memoryStream, new JpegEncoder());
                var base64String = Convert.ToBase64String(memoryStream.ToArray());
                return base64String;
            }
        }
    }
}
