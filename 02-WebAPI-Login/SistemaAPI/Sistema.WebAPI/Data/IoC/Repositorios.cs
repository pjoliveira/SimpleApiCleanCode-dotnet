using SistemaAPI.Data.Repositorios;
using SistemaAPI.Dominio.Servicos;


namespace SistemaAPI.Data.IoC;

public static class Repositorios
{

    /// <summary>
    /// Metodo de Extensão que será utilizado no ConfigureServices do Startup.cs
    /// Adciona as classe da camada InfraData
    /// services.AddRespositorios(Configuration);
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddRespositorios(this IServiceCollection services)
    {

        // Interface e os Repositories
        //***********************************************************************************
        //services.AddScoped<IEmpresaRespositorio, EmpresaRespositorio>();
        //services.AddScoped<ISessao, SessaoServico>();
        //services.AddScoped<IEmail, Email>();
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
        //services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

        // Interface e os Serviços
        //***********************************************************************************
        services.AddScoped<IClasseServico, ClasseServico>();
		services.AddScoped<IUsuarioServico, UsuarioServico>();

		return services;
    }
}
