using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDto
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email { get; set; }
    }
}