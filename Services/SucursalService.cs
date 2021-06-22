using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;

namespace WebAPIGrupo3.Services
{
    public class SucursalService : ISucursalService
    {
        readonly FirestoreConn conn = new();
        public SucursalService()
        {

        }

        public async Task<Sucursal> GetAgency(string id)
        {
            var document = await conn.GetSingleDocumentAsync("sucursales", id);
            Sucursal sucursal = document?.ConvertTo<Sucursal>();
            return sucursal;
        }

        public async Task<IEnumerable<Sucursal>> GetAllAgencies()
        {
            IList<Sucursal> lista = new List<Sucursal>();
            foreach (var document in await conn.GetCollectionAsync("sucursales"))
            {
                Sucursal proc = document.ConvertTo<Sucursal>();
                lista.Add(proc);
            }
            return lista;            
        }
    }
    public interface ISucursalService
    {
        Task<IEnumerable<Sucursal>> GetAllAgencies();
        Task<Sucursal> GetAgency(string id);
    }
}
