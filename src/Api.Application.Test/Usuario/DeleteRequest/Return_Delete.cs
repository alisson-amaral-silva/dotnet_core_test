using System;
using System.Threading.Tasks;
using Api.application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.DeleteRequest
{
    public class Return_Delete
    {
        private UsersController _controller;

        [Fact(DisplayName = "Return Delete")]
        public async Task Return_Controller_Delete()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var returnUpdatedUser = ((OkObjectResult)result).Value;
            Assert.NotNull(returnUpdatedUser);
            Assert.True((Boolean)returnUpdatedUser);
        }

    }
}