using GripCaseStudy.Controllers;
using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Services.Concretes;
using GripCaseStudy.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GripCaseStudyTest
{
    public class ImageService_Tests
    {
        [Fact]
        public void CreateThumbnail_Should_Return_OkResult()
        {
            // Arrange
            var mockImageRepository = new Mock<IImageRepository>();
            var mockImageService = new Mock<IImageService>();
            var mockUserService = new Mock<IUserService>();

            mockImageService.Setup(x => x.GenerateThumbnailBase64(It.IsAny<Stream>())).Verifiable();

            var controller = new ImageController(mockImageRepository.Object,mockUserService.Object,mockImageService.Object);

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(x => x.OpenReadStream()).Returns(Stream.Null);

            // Act
            var result = controller(formFileMock.Object);

            // Assert
            Assert.IsType<OkResult>(result);
            mockImageService.Verify(x => x.GenerateThumbnailBase64(It.IsAny<Stream>()), Times.Once);
        }
    }
}
