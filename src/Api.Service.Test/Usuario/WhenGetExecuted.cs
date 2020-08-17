using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class WhenGetExecuted : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "It's possible to execute GET Method.")]
        public async Task Possible_Execute_Get_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(UserId)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(UserId);
            Assert.NotNull(result);
            Assert.Equal(Username, result.Name);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto) null));
            _service = _serviceMock.Object;

            var _nullRequest = await _service.Get(UserId);
            Assert.Null(_nullRequest);

        }
    }
}