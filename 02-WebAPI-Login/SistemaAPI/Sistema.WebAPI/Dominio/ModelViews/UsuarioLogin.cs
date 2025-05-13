using System.ComponentModel.DataAnnotations;

namespace SistemaAPI.Dominio.ModelViews;

public class UsuarioLogin
{
	[Required, EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Senha { get; set; } = string.Empty;
}
