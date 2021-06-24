using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIGrupo3.Models
{
    [FirestoreData]
    public class Sucursal
    {
        public Sucursal()
        {

        }

        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string NombreSucursal { get; set; }
        [FirestoreProperty] public string DireccionSucursal { get; set; }
        [FirestoreProperty] public string TelefonoSucursal { get; set; }

        public override string ToString()
        {
            return NombreSucursal;
        }
    }
}
