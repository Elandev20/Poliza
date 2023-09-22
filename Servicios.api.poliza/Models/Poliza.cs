using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Servicios.api.poliza.Models
{
    public class Poliza
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string NumeroPoliza { get; set; }

        public string NombreCliente { get; set; }

        public int Identificacion { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaTomaPoliza { get; set; }

        public DateTime FechaFinPoliza { get; set; }

        public string Coberturas { get; set; }

        public int ValorMaximo { get; set; }

        public string NombrePoliza { get; set; }

        public string CiudadCliente { get; set; }

        public string DireccionCliente { get; set; }

        public string Placa { get; set; }

        public string Modelo { get; set; }

        public Boolean Inspeccion { get; set; }

        public Boolean Vigente { get; set; }
    }
}
