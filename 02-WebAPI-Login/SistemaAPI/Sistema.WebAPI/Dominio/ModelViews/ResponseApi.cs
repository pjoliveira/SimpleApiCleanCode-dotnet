namespace SistemaAPI.Dominio.ModelViews;

public class ResponseApi<T> where T : class
{
	public int StatusCode { get; set; } // Código de status HTTP
	public string Mensagem { get; set; } = string.Empty; // Mensagem adicional
	public T? Dados { get; set; }         // Dados retornados	

}
