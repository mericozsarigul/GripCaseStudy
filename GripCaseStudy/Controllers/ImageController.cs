﻿using GripCaseStudy.Models;
using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace GripCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUserService _userService;

        public ImageController(IImageRepository imageRepository, IUserService userService)
        {
            _imageRepository = imageRepository;
            _userService = userService;
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

                    await _imageRepository.Create(new ImageModel
                    {
                        UserId = user.Id,
                        ImageBase64 = imgBase64
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userService.GetUserFromToken(Request.Headers[HeaderNames.Authorization].ToString());
                if (user == null)
                    return Unauthorized();

                var images = await _imageRepository.GetUserAllImages(user.Id);

                return Ok(new { images });

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

                return Ok(new { image.ImageBase64 });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
