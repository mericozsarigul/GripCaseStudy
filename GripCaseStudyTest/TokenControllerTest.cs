using GripCaseStudy.Controllers;
using GripCaseStudy.Repositories.Concretes;
using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Services.Concretes;
using GripCaseStudy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GripCaseStudyTest
{
    public class TokenControllerTest
    {
        private readonly TokenController _controller;
        private readonly IUserService _service;
        private readonly IUserRepository _repo;

        public TokenControllerTest()
        {
            _repo = new UserRepository();
            _service = new UserService();
            _controller = new ShoppingCartController(_service);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
    }
}
}
