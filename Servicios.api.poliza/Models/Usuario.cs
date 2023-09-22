using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Servicios.api.poliza.Models
{
    public class Usuario
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
    }
}
