using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAPI.Dominio.Entidades;

/// <summary>
/// Classes de Dominio 
/// </summary>

[Table("dcclasses")]
public class Classe : BaseEntidade
{
	[Required(AllowEmptyStrings = false, ErrorMessage = "Informe o código.")]
	[MaxLength(6)]
	[Display(Name = "Código")]
	public string Codigo { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "Informe a descrição.")]
    [MaxLength(50)]
    [MinLength(2)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    //public ICollection<BeneficioEntidade> Beneficios { get; set; }

}
