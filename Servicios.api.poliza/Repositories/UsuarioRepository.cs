using MongoDB.Driver;
using Servicios.api.poliza.Models;

namespace Servicios.api.poliza.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        internal MongoDBRepository _repository = new MongoDBRepository();

        private IMongoCollection<Usuario> Collection;

        public UsuarioRepository()
        {
            Collection = _repository.db.GetCollection<Usuario>("Usuario");
        }
        public   Usuario ValidateUser(Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.And(
                Builders<Usuario>.Filter.Eq(x => x.correo, usuario.correo),
                Builders<Usuario>.Filter.Eq(x => x.clave, usuario.clave));
            // var filter = Builders<Poliza>.Filter.Eq(x => x.NumeroPoliza, polizaId || );
            return  Collection.FindAsync(filter).Result.FirstOrDefault();

        }
    }
}
