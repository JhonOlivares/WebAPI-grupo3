using Google.Cloud.Firestore;
using System;

namespace WebAPIGrupo3.Models
{
    [FirestoreData]
    public class Estancia
    {
        public Estancia()
        {

        }

        public Estancia(DateTimeOffset fechaentrada, DateTimeOffset fechasalida, Hotel hotel)
        {
            FechaEntrada = fechaentrada;
            FechaSalida = fechasalida;
            Hotel = hotel;
        }

        //[FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public DateTimeOffset FechaEntrada { get; set; }
        [FirestoreProperty] public DateTimeOffset FechaSalida { get; set; }
        [FirestoreProperty] public Hotel Hotel { get; set; }

    }
}
