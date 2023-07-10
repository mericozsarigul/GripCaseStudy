using GripCaseStudy.Models;
using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace GripCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public ImageController(IImageRepository imageRepository, IUserService userService, IImageService imageService)
        {
            _imageRepository = imageRepository;
            _userService = userService;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Please select file.");

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string imgBase64 = Convert.ToBase64String(fileBytes);
                    var user = await _userService.GetUserFromToken(Request.Headers[HeaderNames.Authorization].ToString());

                    if(user != null)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            var thumbnail = _imageService.GenerateThumbnailBase64(stream);

                            await _imageRepository.Create(new ImageModel
                            {
                                UserId = user.Id,
                                ImageBase64 = imgBase64,
                                ImageThumbBase64 = thumbnail
                            });
                        }                        
                    }
                    else
                        return Unauthorized();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userService.GetUserFromToken(Request.Headers[HeaderNames.Authorization].ToString());
                if (user == null)
                    return Unauthorized();

                var images = await _imageRepository.GetUserAllImages(user.Id);

                if (images.Any())
                {
                    var response = images.Select(s=>s.Id).ToList();
                    return Ok(new { response });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var image = await _imageRepository.GetById(id);

                if (image == null)
                    return NotFound();

                return Ok(new { image.ImageBase64,image.ImageThumbBase64 });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
