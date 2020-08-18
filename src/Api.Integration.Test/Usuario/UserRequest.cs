using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Usuario
{
    public class UserRequest : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task Possible_to_Execute_User_Crud()
        {
            await AdicionarToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDto()
            {
                Name = _name,
                Email = _email
            };

            await PostTest(userDto);

            await GetAllTest();

            await UpdateTest(userDto);

        }

        private async Task UpdateTest(UserDto userDto)
        {
            var updateUserDto = new UserDto()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(
                JsonConvert.SerializeObject(updateUserDto),
                Encoding.UTF8,
                "application/json"
                );

            var id = Guid.NewGuid();
            var endpoint = await client.PutAsync($"{hostApi}users/{id}", stringContent);
            var result = await response.Content.ReadAsStringAsync();
            var updatedUser = JsonConvert.DeserializeObject<UserDtoUpdateResult>(result);
            Assert.Equal(HttpStatusCode.OK, endpoint.StatusCode);
            Assert.NotEqual(userDto.Name, updatedUser.Name);
            Assert.NotEqual(userDto.Email, updatedUser.Email);

            await GetByIdTest(id);

            await DeleteTest(id);
        }

        private async Task DeleteTest(Guid id)
        {
            var endpoint = await client.DeleteAsync($"{hostApi}users/{id}");
            Assert.Equal(HttpStatusCode.OK, endpoint.StatusCode);

            endpoint = await client.GetAsync($"{hostApi}users/{id}");
            Assert.Equal(HttpStatusCode.NotFound, endpoint.StatusCode);
        }

        private async Task GetByIdTest(Guid id)
        {
            response = await client.GetAsync($"{hostApi}users/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var selectedUser = JsonConvert.DeserializeObject<UserDto>(jsonResult);
            Assert.NotNull(selectedUser);
        }

        private async Task GetAllTest()
        {
            var endpoint = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, endpoint.StatusCode);

            var response = await endpoint.Content.ReadAsStringAsync();
            var userList = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(response);
            Assert.NotNull(userList);
            Assert.True(userList.Count() > 0);
        }

        private async Task PostTest(UserDto userDto)
        {
            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDto>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, registroPost.Name);
        }
    }
}