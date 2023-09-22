using Servicios.api.poliza.Models;

namespace Servicios.api.poliza.Repositories
{
    public interface IPolizaCollection
    {
        Task<Response> InsertPoliza(Poliza poliza);

        Task UpdatePoliza(Poliza poliza);

        Task DeletePoliza(string polizaId);

        Task<List<Poliza>> GetAllPoliza();

        Task<Poliza> GetPolizaById(string polizaId);
    }
}
