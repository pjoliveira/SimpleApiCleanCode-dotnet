using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace SistemaAPI.Dominio.ModelViews;

public class Requisicao
{
	[JsonPropertyOrder(0)]
	public string Uid { get; set; } = string.Empty; // UId do registro a ser atualizado

}

public class RequisicaoDados<T> : Requisicao
{
	[JsonPropertyOrder(1)]
	public T? Dados { get; set; }
}

