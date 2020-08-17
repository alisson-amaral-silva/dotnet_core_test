using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.GetAllRequest
{
    public class Return_Bad_Request
    {

        private UsersController _controller;

        [Fact(DisplayName = "Return BadRequest Get All")]
        public async Task Return_Controller_Bad_Request_Get_All()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UserDto>
                {
                    new UserDto
                    {
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                    },
                    new UserDto
                    {
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                    }
                }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Format invalid");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);

        }

    }
}