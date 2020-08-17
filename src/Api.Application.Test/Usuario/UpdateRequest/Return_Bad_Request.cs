using System;
using System.Threading.Tasks;
using Api.application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.UpdateRequest
{
    public class Return_Bad_Request
    {
        private UsersController _controller;

        [Fact(DisplayName = "Return Update")]
        public async Task Return_Controller_Update()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var id = Guid.NewGuid();

            serviceMock.Setup(m => m.Put(id, It.IsAny<UserDto>())).ReturnsAsync(
                new UserDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    ModifiedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "Required Field");

            var userDto = new UserDto
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Put(id, userDto);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

    }
}