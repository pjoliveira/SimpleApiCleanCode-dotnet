using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SistemaAPI.Recursos.Constantes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAPI.Dominio.Entidades;

/// <summary>
/// Classes de Dominio 
/// </summary>

[Table("dcusuarios")]
public class Usuario : BaseEntidade
{
	[Required, EmailAddress]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = string.Empty;
	
	[Required]
	[DataType(DataType.Password)]
	public string Senha { get; set; } = string.Empty;
	
	[Required]
	[StringLength(50)]
	public string Nome { get; set; } = string.Empty;
	
	[StringLength(10)]
	[DisplayName("Tipo")]
	public string Tipo { get; set; } = Genericas.TipoUsuario.Usuario;

	public int ExpiraDias { get; set; } = 0;

}
