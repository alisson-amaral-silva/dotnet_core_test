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
    public class Return_Get_All
    {

        private UsersController _controller;

        [Fact(DisplayName = "Return Get All")]
        public async Task Return_Controller_Get_All()
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

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value as IEnumerable<UserDto>;
            Assert.True(resultValue.Count() > 0);
        }

    }
}