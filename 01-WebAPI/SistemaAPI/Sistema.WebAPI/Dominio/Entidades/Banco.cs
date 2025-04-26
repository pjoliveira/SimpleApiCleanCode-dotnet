using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SistemaAPI.Dominio.Entidades;

/// <summary>
/// Classes de Dominio 
/// </summary>
/// 
[Table("dcBancos")]
public class Banco 
{
	[Key]
	[Required(AllowEmptyStrings = false, ErrorMessage = "O código do banco é obrigatório")]
	[DisplayName("Código Banco")]
	[MinLength(03)]
	[MaxLength(03)]
	public string Codigo { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "O nome do banco é obrigatório")]
	[DisplayName("Descricao do Banco")]
	[MinLength(20)]
	[MaxLength(100)]
	public string Nome { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "O nome reduzido do banco é obrigatório")]
	[DisplayName("Descricao reduzido")]
	[MinLength(02)]
	[MaxLength(40)]
	public string Fantasia { get; set; } = string.Empty;

	[DisplayName("Empresa UId")]
	public long? EmpresaId { get; set; }

}
