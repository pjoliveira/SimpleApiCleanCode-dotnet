using SistemaAPI.Recursos.Constantes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaAPI.Dominio.ModelViews;

public class UsuarioNovo
{
	[Required, EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Senha { get; set; } = string.Empty;

	[Required]
	[StringLength(50)]
	public string Nome { get; set; } = string.Empty;

	[StringLength(10)]
	[DisplayName("Tipo")]
	public string Tipo { get; set; } = Genericas.TipoUsuario.Usuario;

	public int ExpiraDias { get; set; } = 0;
}
