using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;

namespace WebAPIGrupo3.Services
{
    public class VueloService : IVueloService
    {
        readonly FirestoreConn conn = new();
        public VueloService()
        {

        }
        public async Task<IEnumerable<Vuelo>> GetAllFlights()
        {
            IList<Vuelo> lista = new List<Vuelo>();
            foreach (var document in await conn.GetCollectionAsync("vuelos"))
            {
                Vuelo proc = document.ConvertTo<Vuelo>();
                lista.Add(proc);
            }
            return lista;
        }

        public async Task<Vuelo> GetFlight(string id)
        {
            var document = await conn.GetSingleDocumentAsync("vuelos", id);
            Vuelo vuelo = document?.ConvertTo<Vuelo>();
            return vuelo;
        }
    }
    public interface IVueloService
    {
        Task<IEnumerable<Vuelo>> GetAllFlights();
        Task<Vuelo> GetFlight(string id);
    }
}
