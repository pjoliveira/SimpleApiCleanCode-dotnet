
using SistemaAPI.Dominio.Entidades;

namespace SistemaAPI.Dominio.Servicos;

public interface IClasseServico
{
	/// <summary>
	/// Metodo para Listar os dados
	/// </summary>
	/// <returns>ProdutoValorLista com os dados</returns>
	Task<IEnumerable<Classe>> GetListAsync();

	/// <summary>
	/// Metodo para buscar por ID
	/// </summary>
	/// <returns>Dados da ID correpondente</returns>
	Task<Classe> GetUIdAsync(string uid);

	/// <summary>
	/// Metodo adcionar registros
	/// </summary>
	/// <returns>inteiro </returns>
	Task<Classe> AdicionarAsync(Classe item);

	/// <summary>
	/// Metodo de update de registros
	/// </summary>
	/// <returns>inteiro </returns>
	Task<Classe> AtualizarAsync(Classe item);

	/// <summary>
	/// Metodo para deletar registros
	/// </summary>
	/// <returns>inteiro </returns>
	Task<Classe> RemoverAsync(string uid);

	/// <summary>
	/// Metodo para gravar os dados 
	/// </summary>
	/// <returns>inteiro </returns>
	Task<int> SalvarAsync();

	//string SessaoEmpresaUId();

	//bool GetSessaoValida();
}
