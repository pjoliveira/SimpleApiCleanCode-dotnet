using Microsoft.AspNetCore.Http;
using SistemaAPI.Dominio.Entidades;
using SistemaAPI.Data.Repositorios;
using SistemaAPI.Dominio.Exceptions;
using SistemaAPI.Recursos.Extensao;
using SistemaAPI.Recursos.Constantes;
using Microsoft.EntityFrameworkCore;

namespace SistemaAPI.Dominio.Servicos;

public class ClasseServico : IClasseServico
{
	private readonly IRepositorioGenerico<Classe> _repositorio;
	//private readonly ISessao _sessao;

	public ClasseServico(IRepositorioGenerico<Classe> repositorio)
	{
		_repositorio = repositorio; // new RepositorioGenerico<Categoria>(context);
		//_sessao = sessao;

	}

	public async Task<Classe> AdicionarAsync(Classe item)
	{
		if (item.Descricao.IsNullOrEmpty())
		{
			throw new ExceptionValidacaoDados("A Descricao não pode esta em branco.");
		}

		//item.EmpresaUId = SessaoEmpresaUId();
		item.UId = _repositorio.GerarNovoUId();
		item.Codigo = "000000"; // _repositorio.GerarNovoCodigo();

		// verifica se ja existe o Uid cadastrado para outro item.
		if (await _repositorio.CondicaoExisteAsync(e => e.UId == item.UId))
		{
			throw new ExceptionValidacaoDados("Salvar novamente, Id ja existe.");
		}

		// verifica se ja existe a mesma Descricao em um outro item ja cadastrado
		if (await _repositorio.CondicaoExisteAsync(e => e.Descricao == item.Descricao && e.UId != item.UId))
		{
			throw new ExceptionValidacaoDados("Essa descrição já existe em outro cadastro.");
		}

		// Atualiza os atributos 
		//...
		item.RegistroDataCriacao = DateTime.Now;
		item.RegistroDataAlteracao = DateTime.Now;
		//item.RegistroBloqueado = Genericas.Status.Nao;
		//item.RegistroBloqueadoData = null;

		// Adiciona o produto ao repositório
		//...
		var result = await _repositorio.AdicionarAsync(item);

		if (result is null)
		{
			throw new Exception("Erro ao adicionar.");
		}
		// salvar no banco de dados
		//...
		int registrosSalvos = await SalvarAsync();

		return result;
	}

	public async Task<Classe> AtualizarAsync(Classe item)
	{
		if (item.UId is null)
		{
			throw new ExceptionValidacaoDados("O UId não é valido!");
		}
		if (!Guid.TryParse(item.UId, out _)) //(item.UId.Length < 36)
		{
			throw new ExceptionValidacaoDados("O UId não é valido!");
		}
		if (item.Descricao.IsNullOrEmpty())
		{
			throw new ExceptionValidacaoDados("A descrição não pode estar em branco.");
		}
		// verifica a existencia se o Uid existe para poder ser alterado
		if (!await _repositorio.CondicaoExisteAsync(e => e.UId == item.UId))
		{
			throw new ExceptionValidacaoDados("Esse Id não existe. Outro usuário pode ter excuido.");
		}
		// verifica a existencia da descrição ujem outro cadastro.
		if (await _repositorio.CondicaoExisteAsync(e => e.Descricao == item.Descricao && e.UId != item.UId))
		{
			throw new ExceptionValidacaoDados("Essa descrição já existe em outro cadastro.");
		}

		// Atualiza os atributos 
		//...
		item.RegistroDataAlteracao = DateTime.Now;
		
		// remove a data de bloqueio
		item.RegistroBloqueadoData = null;
		// se a propriedade RegistroBloqueado foi alterada para SIM
		// atualiza a data de bloqueio
		if (item.RegistroBloqueado == Genericas.Status.Sim)
		{
			item.RegistroBloqueadoData = DateTime.Now;
		}

		// atualizar o item com metodo do repositório
		var result = _repositorio.Atualizar(item);
	
		// salvar no banco de dados
		//...
		int registrosSalvos = await SalvarAsync();

		return result;
	}

	public async Task<IEnumerable<Classe>> GetListAsync()
	{
		//string empresauid = SessaoEmpresaUId();
		//var result = await _repositorio.GetListAsync(c => c.EmpresaUId == empresauid);
		var result = await _repositorio.GetListAsync(); //c => c.RegistroGrupoFilial == "000.00"

		return result;
		//throw new NotImplementedException();
	}

	public async Task<Classe> GetUIdAsync(string uid)
	{
		//string empresauid = SessaoEmpresaUId();
		//var result = await _repositorio.GetCondicaoAsync(c => c.EmpresaUId == empresauid && c.UId == uid); 
		if (string.IsNullOrEmpty(uid))
		{
			throw new ExceptionValidacaoDados("O UId não é valido!");
		}

		var result = await _repositorio.GetCondicaoAsync(c => c.UId == uid); 

		if (result is null)
		{
			throw new ExceptionValidacaoDados($"Não encontrado o Id: {uid}");
		}

		return result;
		//throw new NotImplementedException();
	}

	public async Task<Classe> RemoverAsync(string uid)
	{
		// buscar se Uid esta 
		var item = await _repositorio.GetCondicaoAsync(c => c.UId == uid);
		
		// verifica se ja existe pra continuar a exclusão
		if (item is null)
		{
			throw new ExceptionValidacaoDados($"Não encontrado o Id: {uid}");
		}

		// Excluir o item 
		_repositorio.Remover(item);

		// salvar no banco de dados
		//...
		int registrosSalvos = await SalvarAsync();

		return item;
	}

	public async Task<int> SalvarAsync()
	{
		try
		{
			// Salva as alterações no repositório
			var result = await _repositorio.SalvarAsync();
			return result;
		}
		catch (DbUpdateException ex)  //DbUpdateConcurrencyException
		{
			// Captura a exceção DbUpdateException e lança uma exceção personalizada para o controller
			throw new ExceptionValidacaoDados(Mensagens.ErroSalvarRegistro, ex);
		}

	}
}
