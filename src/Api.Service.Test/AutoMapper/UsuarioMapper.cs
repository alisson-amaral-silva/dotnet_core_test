using System;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "It's possible to Map UserEntity")]
        public void Possible_Map_User_Entity()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };


            //Model => Entity
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.Email, model.Email);

            var dto = Mapper.Map<UserDto>(entity);
            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(dto.Email, entity.Email);

            var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(entity);
            Assert.Equal(userDtoCreateResult.Name, entity.Name);
            Assert.Equal(userDtoCreateResult.Email, entity.Email);

            var userDtoModifiedResult = Mapper.Map<UserDtoUpdateResult>(entity);
            Assert.Equal(userDtoModifiedResult.Name, entity.Name);
            Assert.Equal(userDtoModifiedResult.Email, entity.Email);

            var userModel = Mapper.Map<UserModel>(dto);
            Assert.Equal(userModel.Name, dto.Name);
            Assert.Equal(userModel.Email, dto.Email);
        }
    }
}