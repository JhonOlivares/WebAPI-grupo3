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

        public Contrato(Turista turista, Hotel vuelo, Sucursal sucursal, Estancia estancia)
        {
            Turista = turista;
            Hotel = vuelo;
            Sucursal = sucursal;
            Estancia = estancia;
        }

        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public Turista Turista { get; set; }
        [FirestoreProperty] public Hotel Hotel { get; set; }
        [FirestoreProperty] public Sucursal Sucursal { get; set; }
        [FirestoreProperty] public Estancia Estancia { get; set; }
    }
}
