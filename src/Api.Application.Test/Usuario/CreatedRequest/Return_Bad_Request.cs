using System;
using System.Threading.Tasks;
using Api.application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.RequestCreated
{
    public class Return_Bad_Request
    {
        private UsersController _controller;
        private readonly string urlLocal = "http://localhost:5000";

        [Fact(DisplayName = "Return BadRequest")]
        public async Task Return_Controller_Bad_Request()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Post(It.IsAny<UserDto>())).ReturnsAsync(
                new UserDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "Required Field");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(urlLocal);
            _controller.Url = url.Object;

            var userDto = new UserDto
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Post(userDto);
            Assert.True(result is BadRequestObjectResult);
        }

    }
}