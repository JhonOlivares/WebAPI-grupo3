﻿using System.Collections.Generic;
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
                Contrato contra = document.ConvertTo<Contrato>();

                // ACTUALIZA LAS PLAZAS///////

                return contra;
            }            

            //List<Vuelo> vuelos = (List<Vuelo>)await vuelosService.GetAllFlights();
            //vuelos = vuelos.Where(v => v.Destino.IndexOf("rata", StringComparison.OrdinalIgnoreCase) != -1).ToList();
            //vuelos = vuelos.Where(v => v.FechaYHora.Equals(contratoA.FechaIda)).ToList();
            //string rata = "";
            //return await Task.FromResult(new Contrato());
            //77else return new Contrato();
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
