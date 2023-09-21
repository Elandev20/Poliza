using MongoDB.Bson;
using MongoDB.Driver;
using Servicios.api.poliza.Models;

namespace Servicios.api.poliza.Repositories
{
    public class PolizaCollection : IPolizaCollection
    {

        internal MongoDBRepository _repository = new MongoDBRepository();

        private IMongoCollection<Poliza> Collection;

        public PolizaCollection()
        {
            Collection = _repository.db.GetCollection<Poliza>("Poliza");
        }
        public async Task DeletePoliza(string polizaId)
        {
            var filter = Builders<Poliza>.Filter.Eq(x => x.Id, new ObjectId(polizaId));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<Poliza>> GetAllPoliza()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Poliza> GetPolizaById(string polizaId)
        {
            return await Collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(polizaId) } }).Result.FirstAsync();
        }

        public async Task InsertPoliza(Poliza poliza)
        {
            await Collection.InsertOneAsync(poliza);
        }

        public async Task UpdatePoliza(Poliza poliza)
        {
            var filter = Builders<Poliza>
                .Filter
                .Eq(x => x.Id, poliza.Id);

            await Collection.ReplaceOneAsync(filter, poliza);
        }
    }
}
