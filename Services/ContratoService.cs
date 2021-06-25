using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGrupo3.DBService;
using WebAPIGrupo3.Models;
using System;

namespace WebAPIGrupo3.Services
{
    public class ContratoService : IContratoService
    {
        readonly FirestoreConn conn = new();
        readonly VueloService vuelosService = new();
        readonly HotelService hotelService = new();
        readonly SucursalService sucursalService = new();
        readonly TuristaService turistaService = new();

        public async Task<Contrato> Create(ContratoAnonimo jsonObject)
        {
            if (string.IsNullOrEmpty(jsonObject.NumeroVuelo.Trim()) ||
                string.IsNullOrEmpty(jsonObject.IdHotel.Trim()) ||
                string.IsNullOrEmpty(jsonObject.IdTurista.Trim()) ||
                string.IsNullOrEmpty(jsonObject.IdSucursal.Trim()) ||
                string.IsNullOrEmpty(jsonObject.NumeroVuelo.Trim()) ||
                jsonObject.FechaIda.Equals(null) ||
                jsonObject.FechaRegreso.Equals(null))
                return null;

            Vuelo vuelo = await vuelosService.GetFlight(jsonObject.NumeroVuelo);
            Hotel hotel = await hotelService.GetHotel(jsonObject.IdHotel);
            Sucursal sucursal = await sucursalService.GetAgency(jsonObject.IdSucursal);
            Turista turista = await turistaService.GetTurista(jsonObject.IdTurista);

            if (vuelo == null || hotel == null || sucursal == null || turista == null)
                return null;

            if (vuelo.PlazasTuristas < 1 || hotel.PlazasHotel < 1)
                return null;

            if (!vuelo.Destino.Equals(hotel.CiudadHotel, StringComparison.OrdinalIgnoreCase))
                return null;

            if (!vuelo.FechaYHora.Date.Equals(jsonObject.FechaIda.Date))
                return null;

            Estancia estancia = new(jsonObject.FechaIda, jsonObject.FechaRegreso, hotel);
                       

            Contrato contrato = new (turista, vuelo, sucursal, estancia);

            var document = await conn?.UploadDocToFirestore("contratos", contrato, null);
            if (document == null)
                return null;
            else
            {
                Contrato contratofinal = document.ConvertTo<Contrato>();

                int pt = vuelo.PlazasTuristas - 1;
                Dictionary<string, object> dicturista = new();
                dicturista.Add("PlazasTuristas", pt);
                await conn.UpdateGenericDocumentField("vuelos", vuelo.NumeroVuelo, dicturista);


                int ph = hotel.PlazasHotel - 1;
                Dictionary<string, object> dichotel = new();
                dichotel.Add("PlazasHotel", ph);
                await conn.UpdateGenericDocumentField("hoteles", hotel.Id, dichotel);

                return contratofinal;
            }            

        }

        public async Task<IEnumerable<Contrato>> GetContratos(string idTurista)
        {
            IList<Contrato> lista = new List<Contrato>();
            foreach (var document in await conn.GetCollectionAsync("contratos"))
            {
                Contrato proc = document.ConvertTo<Contrato>();
                lista.Add(proc);
            }
            lista = lista.Where(c => c.Turista.Id.Equals(idTurista)).ToList();
            return lista;
        }
    }

    public interface IContratoService
    {
        Task<IEnumerable<Contrato>> GetContratos(string idTurista);
        Task<Contrato> Create(ContratoAnonimo contratoA);
    }
}
