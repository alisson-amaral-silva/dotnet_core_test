using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repositories;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        private SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {

            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);
                if (baseUser == null)
                {
                    return FalharAutenticacao();
                }
                else
                {
                    ClaimsIdentity identity = FormatarIdentity(user);

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SucessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return FalharAutenticacao();
            }
        }

        private ClaimsIdentity FormatarIdentity(LoginDTO user)
        {
            return new ClaimsIdentity(
                new GenericIdentity(user.Email),
                new[]
                {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                }
            );
        }

        private object FalharAutenticacao()
        {
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };
        }

        private object SucessObject(DateTime createDate, DateTime expirationDate, string token, LoginDTO user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso"
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationsDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationsDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}