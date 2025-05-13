using SistemaAPI.Recursos.Constantes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAPI.Dominio.Entidades;

[Table("tgEmpresa")]
public class Empresa 
{
    [Key]
    [DisplayName("UId")]
    [Required(ErrorMessage = "Informe o UId.")]
    public string? Id { get; set; }
   
    [StringLength(100)]
    [Required(ErrorMessage = "Informe a razão social.")]
    [DisplayName("Descricao")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(100)]
    [Required(ErrorMessage = "Informe o nome fantasia.")]
    [DisplayName("Fantasia")]
    public string Fantasia { get; set; } = string.Empty;

    [DisplayName("Criado em")]
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;

    [DisplayName("Atualizado em")]
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;

    [StringLength(03)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "É preciso informar SIM ou NAO.")]
    [DisplayName("Status")]
    public string Ativo { get; set; } = Genericas.Status.Sim; 
}
