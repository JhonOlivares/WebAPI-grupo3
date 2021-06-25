using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;
using System;

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
                Vuelo _vuelo = document.ConvertTo<Vuelo>();
                lista.Add(_vuelo);
            }
            return lista;
        }

        public async Task<IEnumerable<Vuelo>> GetAvailableFlights()
        {
            IList<Vuelo> lista = new List<Vuelo>();
            foreach (var document in await conn.GetCollectionAsync("vuelos"))
            {
                Vuelo _vuelo = document.ConvertTo<Vuelo>();
                if(_vuelo.PlazasTuristas > 0)
                    lista.Add(_vuelo);
            }
            return lista;
        }

        public async Task<IEnumerable<Vuelo>> GetAvailableFlightsByOrigin(string origin)
        {
            IList<Vuelo> lista = new List<Vuelo>();
            foreach (var document in await conn.GetCollectionAsync("vuelos"))
            {
                Vuelo _vuelo = document.ConvertTo<Vuelo>();
                if (_vuelo.PlazasTuristas > 0 && _vuelo.Origen.IndexOf(origin, StringComparison.OrdinalIgnoreCase) != -1)
                    lista.Add(_vuelo);
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
        Task<IEnumerable<Vuelo>> GetAvailableFlights();
        Task<IEnumerable<Vuelo>> GetAvailableFlightsByOrigin(string origin);
        Task<Vuelo> GetFlight(string id);
    }
}
