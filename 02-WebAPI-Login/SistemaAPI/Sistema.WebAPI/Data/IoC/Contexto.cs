using Microsoft.EntityFrameworkCore;
using SistemaAPI.Data.Contexto;

namespace SistemaAPI.Data.IoC;

public static class Contexto
{
    /// <summary>
    /// Metodo de Extensão que será utilizado no ConfigureServices do Startup.cs
    /// services.Connection(Configuration);
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddConnection(this IServiceCollection services,
        IConfiguration configuration)
    {
        // a string esta no appsettings.json
        //***********************************************************************************
        var conectionString = configuration.GetConnectionString("SqlServerConnection");

		// conexão com o SQLite
		//***********************************************************************************
		//services.AddDbContext<ApiContexto>(options =>
		//                options.UseSqlite(conectionString,
		//                      b => b.MigrationsAssembly(typeof(ApiContexto).Assembly.FullName)));
		//SQLitePCL.Batteries.Init();
		//services.AddDbContext<ApiContexto>(options =>
		//                      options.UseSqlite(conectionString));


		// conexão com MySql
		//***********************************************************************************
		//var serverVersion = ServerVersion.AutoDetect(conectionString);
		//services.AddDbContext<SistemaContexto>(options =>
		//          options.UseMySql(conectionString, serverVersion));

		// Conexão com o SQL Server
		//***********************************************************************************
		services.AddDbContext<ApiContexto>(options =>
						options.UseSqlServer(conectionString,
						b => b.MigrationsAssembly(typeof(ApiContexto).Assembly.FullName)));

		return services;
    }
}
