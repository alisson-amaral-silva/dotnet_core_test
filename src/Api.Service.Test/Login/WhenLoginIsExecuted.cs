using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class WhenLoginIsExecuted
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;


        [Fact(DisplayName = "It's possible to execute Login Method.")]
        public async Task Possible_Execute_Login_Method()
        {
            var email = Faker.Internet.Email();
            var loginReturn = new
            {
                authenticated = true,
                created = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = Guid.NewGuid(),
                userName = email,
                message = "Usuário Logado com sucesso"
            };

            var loginDto = new LoginDTO {
                Email = email
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(loginReturn);
            _service = _serviceMock.Object;
            
            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);
        }
    }
}