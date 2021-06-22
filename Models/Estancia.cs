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

        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public DateTimeOffset FechaEntrada { get; set; }
        [FirestoreProperty] public DateTimeOffset FechaSalida { get; set; }
        [FirestoreProperty] public Hotel Hotel { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
