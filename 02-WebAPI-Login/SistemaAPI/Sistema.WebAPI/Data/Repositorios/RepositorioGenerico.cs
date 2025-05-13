using Microsoft.EntityFrameworkCore;
using SistemaAPI.Data.Contexto;
using SistemaAPI.Dominio.Entidades;
using SistemaAPI.Recursos.GerarCodigos;
using System.Linq.Expressions;

namespace SistemaAPI.Data.Repositorios;

/// <summary>
/// Classes dos Repositorios de dados
/// </summary>
public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : BaseEntidade
{
    private readonly ApiContexto _contexto;
    protected readonly DbSet<T> _DbSet;

    public RepositorioGenerico(ApiContexto contexto)
    {
        _contexto = contexto;
        _DbSet = contexto.Set<T>();
    }

    public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? condicao = null,
										int numeroPagina = 1,
										int tamanhoPagina = 10,
										params string[] includes)
    {

        //var result = await _DbSet.Where(l => l.EmpresaUId == empresauid).ToListAsync();
        var query = condicao is not null 
                    ? _DbSet.Where(condicao)
                    : _DbSet ; //l => l.EmpresaUId == empresauid

        if (includes is not null && includes.Length > 0)
        {

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

        }

        // Aplica paginação
        query = query.Skip((numeroPagina - 1) * tamanhoPagina).Take(tamanhoPagina);

        // Converte para uma Lista
		var result = await query.ToListAsync();

        // Garente que o result não seja null
        result ??= new();

        return result;
    }

    public async Task<T?> GetCondicaoAsync(Expression<Func<T, bool>> condicao, params string[] includes)
    {
        //var result = await _DbSet.FirstOrDefaultAsync(condicao);  //e => e.EmpresaUId == empresa && e.UId == uid
        //return result;

        //var result = await _DbSet.Where(l => l.EmpresaUId == empresauid).ToListAsync();
        var query = _DbSet.Where(condicao); //l => l.EmpresaUId == empresauid

        if (includes is not null)
        {

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

        }

		T? result = await query.FirstOrDefaultAsync();

		return result;
    }

    public async Task<bool> CondicaoExisteAsync(Expression<Func<T, bool>> condicao)
    {
        //_repositorioGenerico.ExistePorCondicaoAsync(e => e.EmpresaUId == empresa && e.Codigo == codigo);
        return await _DbSet.AnyAsync(condicao);  // e => e.EmpresaUId == empresa && e.UId == uid

    }
    public async Task<T?> AdicionarAsync(T item)
    {
        await _contexto.AddAsync(item);
        return item;
    }

    public T Atualizar(T item)
    {
		// não se faz necessario usar async
		// apenas marcar em memoria o objeto como modificado
		// SaveChangesAsync é que realiza a persistencia.
		_contexto.Update(item);
        return item;
    }

    public T Remover(T item)
    {
		// não se faz necessario usar async
		// apenas marcar em memoria o objeto como deletado
		// SaveChangesAsync é que realiza a persistencia.
		_contexto.Remove(item);
        return item;
    }

    public async Task<int> SalvarAsync()
    {
        return await _contexto.SaveChangesAsync();
    }

    public string GerarNovoUId()
    {
        return GerarGuids.NovoGuid().ToString();
    }

    //public async Task<string> GerarNovoCodigoAsync(string empresaId, string entidadeNome)
    //{
    //    var parameterEmpresaId = new MySqlParameter("@empresaId", empresaId);
    //    var parameterEntidadeNome = new MySqlParameter("@entidadeNome", entidadeNome);
    //    var result = await _contexto.Set<ProcedureResult>()
    //        .FromSqlRaw("CALL GerarNovoCodigo(@empresaId, @entidadeNome)", parameterEmpresaId, parameterEntidadeNome)
    //        .ToListAsync();
    //    return result.FirstOrDefault()?.CodigoFormatado;
    //}


}
