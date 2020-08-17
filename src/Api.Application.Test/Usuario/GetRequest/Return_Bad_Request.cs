using System;
using System.Threading.Tasks;
using Api.application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.GetRequest
{
    public class Return_Bad_Request
    {
        private UsersController _controller;

        [Fact(DisplayName = "Return Get BadRequest")]
        public async Task Return_Controller_Get_Bad_Request()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UserDto
                {
                    Name = name,
                    Email = email,
                }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Format invalid");

            var result = await _controller.Get(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);

        }

    }
}