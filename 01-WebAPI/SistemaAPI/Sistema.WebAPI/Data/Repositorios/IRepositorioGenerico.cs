using SistemaAPI.Dominio.Entidades;
using System.Linq.Expressions;

namespace SistemaAPI.Data.Repositorios;

public interface IRepositorioGenerico<T> where T : BaseEntidade
{

    /// <summary>
    /// Metodo para Lista com os dados
    /// </summary>
    /// <returns>ProdutoValorLista com os dados</returns>
    Task<IEnumerable<T>> GetListAsync(
                                        Expression<Func<T, bool>>? condicao = null,
										int numeroPagina = 1,
	                                    int tamanhoPagina = 10, 
                                        params string[] includes);

    /// <summary>
    /// Metodo para buscar por uma condição.
    /// </summary>
    /// <returns>Dados que correspondam com a condição informada</returns>
    Task<T?> GetCondicaoAsync(Expression<Func<T, bool>> condicao, params string[] includes);

	/// <summary>
	/// Metodo para verificar se ja exsite o ID
	/// </summary>
	/// <returns>Retorna Verdadeiro se atender a condição informada.</returns>
	Task<bool> CondicaoExisteAsync(Expression<Func<T, bool>> condicao);

	/// <summary>
	/// Metodo adcionar registros
	/// </summary>
	/// <returns>Retorna o objeto adicionado.</returns>
	Task<T?> AdicionarAsync(T item);

    /// <summary>
    /// Metodo de atualizar registros
    /// </summary>
    /// <returns>Retorna o objeto atualizado. </returns>
    T Atualizar(T item);

    /// <summary>
    /// Metodo para remover registros
    /// </summary>
    /// <returns>Retorna o objeto removido. </returns>
    T Remover(T item);

    /// <summary>
    /// Metodo para salvar os dados 
    /// </summary>
    /// <returns>Retorna quantos registros foram aplicados. </returns>
    Task<int> SalvarAsync();

    /// <summary>
    /// Metodo para gerar um novo UId 
    /// </summary>
    /// <returns>Retorna um novo UId gerado.</returns>
    string GerarNovoUId();

    /// <summary>
    /// Metodo para gerar um novo Codigo sequencial por EmpresaUId
    /// </summary>
    /// <returns>Retorna um novo Codigo sequencial.</returns>
    //Task<string> GerarNovoCodigoAsync(string empresaId, string entidadeNome);



}
