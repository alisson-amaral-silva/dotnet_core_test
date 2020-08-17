using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class WhenDeleteExecuted : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "It's possible to execute Delete Method.")]
        public async Task Possible_Execute_Delete_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(UserId)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var deletedUser = await _service.Delete(UserId);
            Assert.True(deletedUser);
        }
    }
}