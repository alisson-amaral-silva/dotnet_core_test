using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class WhenPutExecuted : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "It's possible to execute Put Method.")]
        public async Task Possible_Execute_Put_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userDto)).ReturnsAsync(userDtoCreateResult);
            _service = _serviceMock.Object;

            var createdUser = await _service.Post(userDto);
            Assert.NotNull(createdUser);
            Assert.Equal(Username, createdUser.Name);
            Assert.Equal(UserEmail, createdUser.Email);


            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(UserId,userDto)).ReturnsAsync(userDtoUpdateResult);
            _service = _serviceMock.Object;

            var modifiedUser = await _service.Put(UserId,userDto);
            Assert.NotNull(modifiedUser);
            Assert.Equal(UpdatedUsername, modifiedUser.Name);
        }
    }
}