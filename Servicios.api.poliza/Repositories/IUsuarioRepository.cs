using Servicios.api.poliza.Models;

namespace Servicios.api.poliza.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario ValidateUser(Usuario usuario);
    }
}
