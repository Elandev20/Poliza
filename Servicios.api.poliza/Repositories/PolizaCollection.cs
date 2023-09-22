using Amazon.Runtime.Internal.Util;
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
            var filter = Builders<Poliza>.Filter.Or(
                Builders<Poliza>.Filter.Eq(x => x.NumeroPoliza, polizaId),
                Builders<Poliza>.Filter.Eq(x => x.Placa, polizaId));
            return await Collection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<Response> InsertPoliza(Poliza poliza)
        {
            Response objResponse = new Response();
            try
            {
                var filter = Builders<Poliza>.Filter.And(
                Builders<Poliza>.Filter.Eq(x => x.Identificacion, poliza.Identificacion),
                Builders<Poliza>.Filter.Eq(x => x.Vigente, poliza.Vigente));
                Poliza result = await Collection.FindAsync(filter).Result.FirstAsync();
                if (result == null)
                {
                    await Collection.InsertOneAsync(poliza);
                    objResponse.Status = 200;
                    objResponse.Message = "Poliza ingresada";
                }
                else
                {
                    objResponse.Status=400;
                    objResponse.Message = $"El cliente {poliza.Identificacion} ya tiene una poliza activa"; 
                }
                return objResponse;
            }
            catch (Exception)
            {

                throw;
            }


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
