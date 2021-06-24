using Google.Cloud.Firestore;
using System;

namespace WebAPIGrupo3.Models
{
    [FirestoreData]
    public class Hotel
    {
        public Hotel()
        {

        }

        [FirestoreDocumentId] [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string NombreHotel { get; set; }
        [FirestoreProperty] public string DireccionHotel { get; set; }
        [FirestoreProperty] public string CiudadHotel { get; set; }
        [FirestoreProperty] public string TelefonoHotel { get; set; }
        [FirestoreProperty] public int PlazasHotel { get; set; }

        private double pensionVar;
        [FirestoreProperty] public double Pension
        {
            get { return pensionVar; }
            set { pensionVar = Math.Round(value, 2); }
        }

        public override string ToString()
        {
            return NombreHotel;
        }
    }
}
