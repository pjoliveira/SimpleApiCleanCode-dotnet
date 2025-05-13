using Azure.Core;
using Microsoft.AspNetCore.Identity;
using SistemaAPI.Data.Repositorios;
using SistemaAPI.Dominio.Entidades;
using SistemaAPI.Dominio.Exceptions;
using SistemaAPI.Dominio.ModelViews;
using SistemaAPI.Recursos.Constantes;

namespace SistemaAPI.Dominio.Servicos;

public class UsuarioServico : IUsuarioServico
{
	private readonly IRepositorioGenerico<Usuario> _repositorio;

	public UsuarioServico(IRepositorioGenerico<Usuario> repositorio)
	{
		_repositorio = repositorio;
	}
	// Método para autenticar um usuário
	public async Task<Usuario?> AutenticarAsync(string email, string senha)
	{
		// recuperar o usuário do banco de dados
		Usuario? result = await _repositorio.GetCondicaoAsync(u => u.Email == email);
		
		// verifica se o usuário existe
		if (result is null)
		{
			throw new ExceptionValidacaoDados("Usuário não autorizado.");
		}

		// criptografar a senha
		// e comparar com o hash da senha salva no banco de dados
		var hasher = new PasswordHasher<Usuario>();
		var resultado = hasher.VerifyHashedPassword(result, result.Senha, senha);

		// verifica se a senha enviada está correta
		if (resultado != PasswordVerificationResult.Success)
		{
			throw new ExceptionValidacaoDados("Usuário ou senha inválidos.");
		}

		// verifica se o usuário está bloqueado
		if (result.RegistroBloqueado == Genericas.Status.Sim)
		{
			throw new ExceptionValidacaoDados("Usuário bloqueado.");
		}

		return result;

	}
	// Método para criar um novo usuário
	public async Task<Usuario> CriarAsync(UsuarioNovo novo)
	{
		var existe = await _repositorio.CondicaoExisteAsync(u => u.Email == novo.Email);
		if (existe)
		{
			throw new ExceptionValidacaoDados("Já existe um usuário cadastrado com esse e-mail.");
		}

		// criptografar a senha
		var hasher = new PasswordHasher<Usuario>();

		// mapear o ModelView para a entidade
		// 
		var usuario = new Usuario();

		usuario.Email = novo.Email;
		usuario.Senha = novo.Senha;  
		usuario.Nome = novo.Nome;
		usuario.Tipo = novo.Tipo;
		usuario.ExpiraDias = novo.ExpiraDias;
					
		usuario.UId = _repositorio.GerarNovoUId();
		usuario.RegistroDataCriacao = DateTime.Now;
		usuario.RegistroDataAlteracao = DateTime.Now;
		usuario.RegistroBloqueado = Genericas.Status.Nao;

		// gerar o hash da senha
		// para salva-la no banco de dados
		usuario.Senha = hasher.HashPassword(usuario, novo.Senha);

		var result = await _repositorio.AdicionarAsync(usuario);
		await _repositorio.SalvarAsync();

		return result;
		//throw new NotImplementedException();
	}
}
