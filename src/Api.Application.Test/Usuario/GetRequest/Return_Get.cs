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
    public class Return_Get
    {
        private UsersController _controller;

        [Fact(DisplayName = "Return Get")]
        public async Task Return_Controller_Get()
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
            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
            
            var returnUser = ((OkObjectResult)result).Value as UserDto;
            Assert.NotNull(returnUser);
            Assert.Equal(name, returnUser.Name);
            Assert.Equal(email, returnUser.Email);

        }

    }
}