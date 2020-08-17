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
    public class Return_Created
    {
        private UsersController _controller;
        private readonly string urlLocal = "http://localhost:5000";

        [Fact(DisplayName = "Return Created")]
        public async Task Request_Controller_Create()
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

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(urlLocal);
            _controller.Url = url.Object;

            var userDto = new UserDto
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Post(userDto);
            Assert.True(result is CreatedResult);

            var returnCreatedUser = ((CreatedResult)result).Value as UserDtoCreateResult;
            Assert.NotNull(returnCreatedUser);
            Assert.Equal(userDto.Name, returnCreatedUser.Name);
            Assert.Equal(userDto.Email, returnCreatedUser.Email);
        }

    }
}