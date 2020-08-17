using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.Usuario
{
    public class UserTests
    {
        public static string Username { get; set; }
        public static string UserEmail { get; set; }
        public static string UpdatedUsername { get; set; }
        public static string UpdatedUserEmail { get; set; }
        public static Guid UserId { get; set; }

        public List<UserDto> listaUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserDtoCreateResult userDtoCreateResult;
        public UserDtoUpdateResult userDtoUpdateResult;

        public UserTests()
        {
            UserId = Guid.NewGuid();
            Username = Faker.Name.FullName();
            UserEmail = Faker.Internet.Email();
            UpdatedUsername = Faker.Name.FullName();
            UpdatedUserEmail = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                listaUserDto.Add(dto);
            }

            userDto = new UserDto
            {
                Name = Username,
                Email = UserEmail
            };

            userDtoCreateResult = new UserDtoCreateResult
            {
                Name = Username,
                Email = UserEmail,
                CreateAt = DateTime.UtcNow
            };

            userDtoUpdateResult = new UserDtoUpdateResult
            {
                Name = UpdatedUsername,
                Email = UpdatedUserEmail,
                ModifiedAt = DateTime.UtcNow
            };
        }
    }
}