using Google.Cloud.Firestore;

namespace WebAPIGrupo3.Models
{
    [FirestoreData]
    public class Turista
    {
        public Turista()
        {

        }

        public Turista(string name, string lastName, string address, string phone)
        {
            Nombre = name;
            Apellido = lastName;
            Direccion = address;
            Telefono = phone;
        }
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public string Nombre { get; set; }
        [FirestoreProperty] public string Apellido { get; set; }
        [FirestoreProperty] public string Direccion { get; set; }
        [FirestoreProperty] public string Telefono { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
