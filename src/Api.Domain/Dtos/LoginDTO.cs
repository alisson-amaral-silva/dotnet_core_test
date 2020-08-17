using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email { get; set; }
    }
}