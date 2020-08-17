using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                await CreateTest(_repository, _entity);

                await UpdateTest(_repository, _entity);

            }
        }

        private async Task UpdateTest(UserImplementation _repository, UserEntity _entity)
        {
            _entity.Name = Faker.Name.First();
            var _updatedUser = await _repository.UpdateAsync(_entity);
            Assert.NotNull(_updatedUser);
            Assert.Equal(_entity.Email, _updatedUser.Email);
            Assert.Equal(_entity.Name, _updatedUser.Name);

            findByLoginTest(_repository, _updatedUser);

            await ExistTest(_repository, _updatedUser);
            
            await selectAllUsersTest(_repository);

            await SelectUserByIdTest(_repository, _updatedUser);


        }

        private static async Task selectAllUsersTest(UserImplementation _repository)
        {
            var _listUsers = await _repository.SelectAsync();
            Assert.True(_listUsers.Count() > 0);
            Assert.NotNull(_listUsers);
        }

        private void findByLoginTest(UserImplementation _repository, UserEntity _updatedUser)
        {
            var _loginUser = _repository.FindByLogin(_updatedUser.Email);
            Assert.NotNull(_loginUser);
        }

        private async Task SelectUserByIdTest(UserImplementation _repository, UserEntity _updatedUser)
        {
            var _selectedUser = await _repository.SelectAsync(_updatedUser.Id);
            Assert.Equal(_updatedUser.Email, _selectedUser.Email);
            Assert.Equal(_updatedUser.Name, _selectedUser.Name);
            Assert.NotNull(_selectedUser);

            await DeleteTest(_repository, _selectedUser);

        }

        private async Task DeleteTest(UserImplementation _repository, UserEntity _selectedUser)
        {
            var _removeu = await _repository.DeleteAsync(_selectedUser.Id);
            Assert.True(_removeu);
        }

        private async Task ExistTest(UserImplementation _repository, UserEntity _updatedUser)
        {
            var _registroExists = await _repository.ExistAsync(_updatedUser.Id);
            Assert.True(_registroExists);
        }

        private async Task CreateTest(UserImplementation _repository, UserEntity _entity)
        {
            var _createdUser = await _repository.InsertAsync(_entity);
            Assert.NotNull(_createdUser);
            Assert.Equal(_entity.Email, _createdUser.Email);
            Assert.Equal(_entity.Name, _createdUser.Name);
            Assert.False(_createdUser.Id == Guid.Empty);
        }
    }
}