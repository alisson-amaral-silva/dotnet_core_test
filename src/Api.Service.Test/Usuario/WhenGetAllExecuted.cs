using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class WhenGetAllExecuted : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "It's possible to execute GETAll Method.")]
        public async Task Possible_Execute_GetAll_Method()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listaUserDto);
            _service = _serviceMock.Object;

            await checkListHasValues();

            var _listResult = new List<UserDto>(); 
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listaUserDto.AsEnumerable);
            _service = _serviceMock.Object;

            var emptyList = await _service.GetAll();
            Assert.Empty(emptyList);
            Assert.True(emptyList.Count() == 0);
        }

        private async Task checkListHasValues()
        {
            var result = await _service.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}