using SistemaAPI.Recursos.Constantes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SistemaAPI.Dominio.Entidades;

public abstract class BaseEntidade
{
	[Key]
	[DisplayName("UId")]
	[Required(ErrorMessage = "Informe o UId")]
	public string? UId { get; set; } = string.Empty;

	[StringLength(6)]
	[DisplayName("Grupo Filial")]
	//[ForeignKey("EmpresaId")]
	public string RegistroGrupoFilial { get; set; } = string.Empty;

	[DisplayName("Criado em")]
    public DateTime RegistroDataCriacao { get; set; } = DateTime.Now;

    [DisplayName("Atualizado em")]
    public DateTime RegistroDataAlteracao { get; set; } = DateTime.Now;

    [StringLength(3)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "É preciso informar SIM ou NAO.")]
    [DisplayName("Bloqueado")]
    public string RegistroBloqueado { get; set; } = Genericas.Ativo.Nao;

	[DisplayName("Bloqueado em")]
	public DateTime? RegistroBloqueadoData { get; set; } 




}
