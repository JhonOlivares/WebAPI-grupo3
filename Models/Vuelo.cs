using System;
using Google.Cloud.Firestore;

namespace WebAPIGrupo3.Models
{
    [FirestoreData]
    public class Vuelo
    {
        public Vuelo()
        {

        }

        [FirestoreDocumentId] public string NumeroVuelo { get; set; }
        [FirestoreProperty] public DateTimeOffset FechaYHora { get; set; }
        [FirestoreProperty] public string Origen { get; set; }
        [FirestoreProperty] public string Destino { get; set; }
        [FirestoreProperty] public int PlazasTotales { get; set; }
        [FirestoreProperty] public int PlazasTuristas { get; set; }
        
        
        private double precioVueloVar;
        [FirestoreProperty] public double PrecioVuelo
        {
            get { return precioVueloVar; }
            set { precioVueloVar = Math.Round(value, 2); }
        }

        public override string ToString()
        {
            return NumeroVuelo;
        }
    }
}
