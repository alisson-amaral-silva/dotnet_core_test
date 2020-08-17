using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class WhenPostExecuted : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "It's possible to execute Post Method.")]
        public async Task Possible_Execute_Post_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userDto)).ReturnsAsync(userDtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.Post(userDto);
            Assert.NotNull(result);
            Assert.Equal(Username, result.Name);
            Assert.Equal(UserEmail, result.Email);
        }
    }
}