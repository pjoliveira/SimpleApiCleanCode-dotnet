using SistemaAPI.Recursos.Constantes;
using SistemaAPI.Recursos.Extensao;
using System.Text.Json.Serialization;

namespace SistemaAPI.Dominio.ModelViews;


public class Resposta
{
	[JsonPropertyOrder(0)]
	public bool BemSucedido { get; protected set; } = false;
	[JsonPropertyOrder(1)]
	public int StatusCode { get; protected set; } // Código de status HTTP
	[JsonPropertyOrder(2)]
	public string Mensagem { get; protected set; } = string.Empty; // Mensagem adicional
	
	public Resposta()
	{
		StatusCode = StatusCodes.Status200OK;
		Mensagem = "Sucesso";
	}

	public Resposta Sucesso()
	{
		BemSucedido = true;
		StatusCode = StatusCodes.Status200OK;
		Mensagem = "Sucesso";
		return this;
	}
	public Resposta Sucesso(string mensagem)
	{
		BemSucedido = true;
		StatusCode = StatusCodes.Status200OK;
		Mensagem = mensagem;
		return this;
	}
	public Resposta NaoEncontrado(string mensagem)
	{
		if (mensagem.IsNullOrEmpty())
		{
			mensagem = "Não encontrado.";
		}
		BemSucedido = false;
		StatusCode = StatusCodes.Status404NotFound;
		Mensagem = mensagem;
		return this;
	}
	public Resposta NaoAutorizado(string mensagem)
	{
		if (mensagem.IsNullOrEmpty())
		{
			mensagem = "Autenticação necessária.";
		}
		BemSucedido = false;
		StatusCode = StatusCodes.Status401Unauthorized;
		Mensagem = mensagem;
		return this;
	}
	public Resposta AcessoNegado(string mensagem)
	{
		if (mensagem.IsNullOrEmpty())
		{
			mensagem = "Acesso negado.";
		}
		BemSucedido = false;
		StatusCode = StatusCodes.Status403Forbidden;
		Mensagem = mensagem;
		return this;
	}

	public Resposta Erro(int statuscode, string mensagem)
	{
		if (mensagem.IsNullOrEmpty())
		{
			mensagem = "Erro interno.";
		}
		BemSucedido = false;
		StatusCode = statuscode;
		Mensagem = mensagem;
		return this;
	}

	public Resposta Erro(string mensagem)
	{
		if (mensagem.IsNullOrEmpty())
		{
			mensagem = "Erro interno.";
		}
		BemSucedido = false;
		StatusCode = StatusCodes.Status500InternalServerError;
		Mensagem = mensagem;
		return this;
	}
}
public class Resposta<T> : Resposta
{
	[JsonPropertyOrder(4)]
	public T? Dados { get; set; }        // Dados retornados	

	public Resposta<T> Sucesso(T dados)
	{
		BemSucedido = true;
		StatusCode = StatusCodes.Status200OK;
		Mensagem = "Sucesso";
		Dados = dados;
		return this;

	}

	public Resposta<T> CriadoComSucesso(T dados)
	{
		BemSucedido = true;
		StatusCode = StatusCodes.Status201Created;
		Mensagem = "Criado com sucesso";
		Dados = dados;
		return this;
	}
	public Resposta<T> AlteradoComSucesso(T dados)
	{
		BemSucedido = true;
		StatusCode = StatusCodes.Status200OK;
		Mensagem = "Alterado com sucesso";
		Dados = dados;
		return this;
	}
	public Resposta<T> ExcluidoComSucesso(T dados)
	{
		BemSucedido = true;
		StatusCode = StatusCodes.Status200OK;
		Mensagem = "Excluido com sucesso";
		Dados = dados;
		return this;
	}

	public new Resposta<T> NaoEncontrado(string mensagem)
	{
		if (mensagem.IsNullOrEmpty())
		{
			mensagem = "Não encontrado.";
		}
		StatusCode = StatusCodes.Status404NotFound;
		Mensagem = mensagem;
		Dados = default;
		return this;
	}


}
