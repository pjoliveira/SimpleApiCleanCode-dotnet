using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAPI.Dominio.Entidades;
using SistemaAPI.Dominio.Exceptions;
using SistemaAPI.Dominio.ModelViews;
using SistemaAPI.Dominio.Servicos;

namespace SistemaAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{
		private readonly IUsuarioServico _servico;
		public UsuariosController(IUsuarioServico servico)
		{
			_servico = servico;
		}

		// criar um metodo para o login do requisicaoLogin
		[HttpPost("Login")]
		[ProducesResponseType(StatusCodes.Status200OK)]    // Retorno 200 (OK)
		[ProducesResponseType(StatusCodes.Status400BadRequest)] // Retorno 400 (Bad Request)
		[ProducesResponseType(StatusCodes.Status404NotFound)] // Retorno 404 (Not Found)
		[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500 (Erro interno)
		public async Task<ActionResult<string>> Login([FromBody] UsuarioLogin requisicaoLogin)
		{
			var resposta = new Resposta();
			var respostaok = new Resposta<Usuario>();

			// verifica se o objeto é valido
			if (!ModelState.IsValid)
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não é válido.");
				return BadRequest(resposta);
			}

			if (requisicaoLogin is null)
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O usuário não informado.");
				return BadRequest(resposta);
			}
			if (string.IsNullOrWhiteSpace(requisicaoLogin.Email) || string.IsNullOrWhiteSpace(requisicaoLogin.Senha))
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O email ou senha não informado.");
				return BadRequest(resposta);
			}


			try
			{
				Usuario? item = await _servico.AutenticarAsync(requisicaoLogin.Email, requisicaoLogin.Senha);
				respostaok.Sucesso(item);
				// Retorna os dados com Status201Created
				return Ok(respostaok);
			}
			catch (ExceptionValidacaoDados ex)
			{
				// Captura de validações de negócio vindas do serviço
				resposta.NaoAutorizado(ex.Message);
				return Unauthorized(resposta);
			}
			catch (Exception ex)
			{
				// Captura de qualquer outro erro 
				resposta.Erro(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, resposta);
			}

	
		}

		// criar um metodo para o cadastro do usuario
		[HttpPost("Criar")]
		[ProducesResponseType(StatusCodes.Status201Created)]    // Retorno 201 (Created)
		[ProducesResponseType(StatusCodes.Status400BadRequest)] // Retorno 400 (Bad Request)
		[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500 (Erro interno)
		public async Task<ActionResult<string>> Criar([FromBody] RequisicaoDados<UsuarioNovo> requisicaoUsuario)
		{
			var resposta = new Resposta();
			var respostaok = new Resposta<Usuario>();

			// verifica se o objeto é valido
			if (!ModelState.IsValid)
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não é válido.");
				return BadRequest(resposta);
			}

			if (requisicaoUsuario is null)
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O usuário não informado.");
				return BadRequest(resposta);
			}

			// verificar se o objeto requisicaoUsuario.Dados é nulo
			if (requisicaoUsuario.Dados is null)
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não pode ser nulo.");
				return BadRequest(resposta);
			}
			
			if (string.IsNullOrWhiteSpace(requisicaoUsuario.Dados?.Email) || string.IsNullOrWhiteSpace(requisicaoUsuario.Dados?.Senha))
			{
				resposta.Erro(StatusCodes.Status400BadRequest, "O email ou senha não informado.");
				return BadRequest(resposta);
			}

			try
			{
				var dados = await _servico.CriarAsync(requisicaoUsuario.Dados);
				respostaok.CriadoComSucesso(dados);
				return CreatedAtAction(nameof(Criar), respostaok);
			}
			catch (ExceptionValidacaoDados ex)
			{
				resposta.Erro(StatusCodes.Status400BadRequest, ex.Message);
				return BadRequest(resposta);
			}
			catch (Exception ex)
			{
				resposta.Erro(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, resposta);
			}
		}
	}
}
