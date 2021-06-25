using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIGrupo3.Models
{
    [FirestoreData]
    public class Contrato
    {
        public Contrato()
        {

        }

        public Contrato(Turista turista, Vuelo vuelo, Sucursal sucursal, Estancia estancia)
        {
            Turista = turista;
            Vuelo = vuelo;
            Sucursal = sucursal;
            Estancia = estancia;
        }

        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public Turista Turista { get; set; }
        [FirestoreProperty] public Vuelo Vuelo { get; set; }
        [FirestoreProperty] public Sucursal Sucursal { get; set; }
        [FirestoreProperty] public Estancia Estancia { get; set; }
    }

    public class ContratoAnonimo
    {
        public ContratoAnonimo()
        {
                
        }

        public string IdTurista { get; set; }
        public string NumeroVuelo { get; set; }
        public string IdSucursal { get; set; }
        public string IdHotel { get; set; }
        public DateTimeOffset FechaEntrada { get; set; }
        public DateTimeOffset FechaSalida { get; set; }
    }
}
