using SistemaAPI.Dominio.Entidades;
using SistemaAPI.Dominio.ModelViews;

namespace SistemaAPI.Dominio.Servicos;

public interface IUsuarioServico
{
	Task<Usuario?> AutenticarAsync(string email, string senha);
	Task<Usuario> CriarAsync(UsuarioNovo novo);
}
