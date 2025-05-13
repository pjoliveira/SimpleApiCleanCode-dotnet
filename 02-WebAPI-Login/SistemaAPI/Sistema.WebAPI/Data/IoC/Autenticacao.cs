using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaAPI.Data.IoC;


public static class Autenticacao
{
    /// <summary>
    /// Metodo de Extensão que será utilizado no ConfigureServices do Startup.cs
    /// Configura o Login com Autenticação e cookies
    /// services.AddAuthentication(Configuration);
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutenticacao(this IServiceCollection services)
    {

        // configurações para trabalhar com login 
        //************************************************
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        //adicionando o serviço de cookies na aplicação
        //************************************************            
        //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/LoginPage";
                options.LogoutPath = "/Login/LogadoPage";
            });

        return services;
    }
}
