using Microsoft.AspNetCore.Mvc;
using SistemaAPI.Dominio.Entidades;
using SistemaAPI.Dominio.Exceptions;
using SistemaAPI.Dominio.ModelViews;
using SistemaAPI.Dominio.Servicos;
using System.Text.Json;


namespace SistemaAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ClassesController : ControllerBase
{
	private readonly IClasseServico _servico;

	public ClassesController(IClasseServico servico)
	{
		_servico = servico;

	}

	// GET: api/<ClassesController>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]    // Retorno 200 (OK)
	[ProducesResponseType(StatusCodes.Status404NotFound)] // Retorno 404 (Not Found)
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500 (Erro interno)
    public async Task<ActionResult<string>> Get()
	{
		var resposta = new Resposta();
		var respostaok = new Resposta<IEnumerable<Classe>>(); // Resposta com dados

		var dados = await _servico.GetListAsync();

		// Verifica se a lista está vazia
		if (dados is null || !dados.Any())
		{
			resposta.NaoEncontrado("Dados não encontrados.");

			return NotFound(resposta);
		}


		respostaok.Sucesso(dados);

		// Retorna os dados com status 200 (OK)
		return Ok(respostaok);

	}

	// GET api/<ClassesController>/5
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]    // Retorno 200 (OK)
	[ProducesResponseType(StatusCodes.Status404NotFound)] // Retorno 404 (Not Found)
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500 (Erro interno)
	public async Task<ActionResult<string>> Get(string id)
	{
		var resposta = new Resposta();
		var respostaok = new Resposta<Classe>();

	
		if (string.IsNullOrWhiteSpace(id))
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O UId não informado.");
			return BadRequest(resposta);
		}
		
		try
		{
			var dados = await _servico.GetUIdAsync(id);
			// Retorna os dados com status 200 (OK)
			respostaok.Sucesso(dados);
			return Ok(respostaok);
		}
		catch (ExceptionValidacaoDados ex)
		{
			// Captura de validações de negócio vindas do serviço
			resposta.NaoEncontrado(ex.Message);
			return NotFound(resposta);
		}
		catch (Exception ex)
		{
			// Captura de erros gerais
			resposta.Erro(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
			return StatusCode(StatusCodes.Status500InternalServerError, resposta);
		}



	}

	// POST api/<ClassesController>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]    // Retorno 201 (Created)
	[ProducesResponseType(StatusCodes.Status400BadRequest)] // Retorno 400 (Bad Request)
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500
	public async Task<ActionResult<string>> Post([FromBody] RequisicaoDados<Classe> item)
	{
		var resposta = new Resposta();
		var respostaok = new Resposta<Classe>();
		
		if (!ModelState.IsValid)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não é válido.");
			return BadRequest(resposta);
		}

		if (item is null) 
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não pode ser nulo.");
			return BadRequest(resposta);
		}

		if (item.Dados is null)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto DADOS não pode ser nulo.");
			return BadRequest(resposta);
		}

		try
		{
			var dados = await _servico.AdicionarAsync(item.Dados);
			respostaok.CriadoComSucesso(item.Dados);
			// Retorna os dados com Status201Created
			return StatusCode(StatusCodes.Status201Created, respostaok);
		}
		catch (ExceptionValidacaoDados ex)
		{
			// Captura de validações de negócio vindas do serviço
			resposta.Erro(StatusCodes.Status400BadRequest, ex.Message);
			return BadRequest(resposta);
		}
		catch (Exception ex)
		{
			// Captura de erros gerais
			resposta.Erro(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
			return StatusCode(StatusCodes.Status500InternalServerError, resposta);
		}


	}

	// PUT api/<ClassesController>/5
	//[HttpPut("{id}")]
	//public void Put(int id, [FromBody] string value)
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status200OK)]    // Retorno 200 (ok)
	[ProducesResponseType(StatusCodes.Status204NoContent)] // Retorno 204 (No Content)
	[ProducesResponseType(StatusCodes.Status400BadRequest)] // Retorno 400 (Bad Request)
	[ProducesResponseType(StatusCodes.Status404NotFound)] // Retorno 404 (Not Found)
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500 (Internal Server Error)
	public async Task<ActionResult<string>> Put([FromBody] RequisicaoDados<Classe> item)
	{
		var resposta = new Resposta();
		var respostaok = new Resposta<Classe>();

		if (!ModelState.IsValid)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não é válido.");
			return BadRequest(resposta);
		}

		if (item is null)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não pode ser nulo.");
			return BadRequest(resposta);
		}

		if (item.Dados is null)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto DADOS não pode ser nulo.");
			return BadRequest(resposta);
		}

		// faz uma chamada ao metodo do repositorio
		// para atualizar os dados recebidos
		try
		{
			var dados = await _servico.AtualizarAsync(item.Dados);
			respostaok.AlteradoComSucesso(item.Dados);
			// Retorna os dados com Status201Created
			return Ok(respostaok);
		}
		catch (ExceptionValidacaoDados ex)
		{
			// Captura de validações de negócio vindas do serviço
			resposta.Erro(StatusCodes.Status400BadRequest, ex.Message);
			return BadRequest(resposta);
		}
		catch (Exception ex)
		{
			// Captura de erros gerais
			resposta.Erro(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
			return StatusCode(StatusCodes.Status500InternalServerError, resposta);
		}

	}

	// DELETE api/<ClassesController>/5
	//[HttpDelete("{id}")]
	//public void Delete(int id)
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status200OK)]    // Retorno 200 (OK)
	[ProducesResponseType(StatusCodes.Status204NoContent)] // Retorno 204 (No Content)
	[ProducesResponseType(StatusCodes.Status400BadRequest)] // Retorno 400 (Bad Request)
	[ProducesResponseType(StatusCodes.Status404NotFound)] // Retorno 404 (Not Found)
	[ProducesResponseType(StatusCodes.Status500InternalServerError)] // Retorno 500 (Internal Server Error)
	public async Task<ActionResult<string>> Delete([FromBody] Requisicao requisicao)
	{
		var resposta = new Resposta();
		var respostaok = new Resposta<Classe>();

		if (!ModelState.IsValid)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não é válido.");
			return BadRequest(resposta);
		}

		if (requisicao is null)
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O objeto não pode ser nulo.");
			return BadRequest(resposta);
		}

		if ((requisicao.Uid is null) || (string.IsNullOrEmpty(requisicao.Uid)))
		{
			resposta.Erro(StatusCodes.Status400BadRequest, "O Id deve ser informado.");
			return BadRequest(resposta);
		}

		
		// faz uma chamada ao metodo do repositorio
		try
		{
			var dados = await _servico.RemoverAsync(requisicao.Uid);

			respostaok.ExcluidoComSucesso(dados);
			// Retorna os dados com Status201Created
			return Ok(respostaok);
		}
		catch (ExceptionValidacaoDados ex)
		{
			// Captura de validações de negócio vindas do serviço
			resposta.Erro(StatusCodes.Status400BadRequest, ex.Message);
			return BadRequest(resposta);
		}
		catch (Exception ex)
		{
			// Captura de erros gerais
			resposta.Erro(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
			return StatusCode(StatusCodes.Status500InternalServerError, resposta);
		}

	}
}
