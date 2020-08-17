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
    public class Return_Bad_Request
    {
        private UsersController _controller;

        [Fact(DisplayName = "Return BadRequest Update")]
        public async Task Return_Controller_Bad_Request_Update()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

    }
}