using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;
using WebAPIGrupo3.Services;

namespace WebAPIGrupo3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VueloController : ControllerBase
    {
        private readonly IVueloService _vueloService;
        public VueloController(IVueloService vueloService)
        {
            _vueloService = vueloService;
        }

        [HttpGet]
        public async Task<IEnumerable<Vuelo>> Get()
        {
            return await _vueloService.GetAvailableFlights();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Vuelo>> Get(string id)
        {
            var vuelo = await _vueloService.GetFlight(id);
            if (vuelo == null)
            {
                return NotFound($"El vuelo: {id} no existe");
            }
            return vuelo;
        }


        [HttpGet("origen/{origen}")]
        public async Task<ActionResult<Vuelo>> Getpororigen(string origen)
        {
            if (string.IsNullOrEmpty(origen?.Trim()))
                return BadRequest("400");

            List<Vuelo> vuelos = (List<Vuelo>)await _vueloService.GetAvailableFlightsByOrigin(origen);
            return Ok(vuelos);
        }


        [HttpGet("por-origen")]
        public async Task<ActionResult> GetByOrigin([FromBody] Vuelo vuelo)
        {
            if (string.IsNullOrEmpty(vuelo.Origen?.Trim()))
                return BadRequest("400");

            List<Vuelo> vuelos = (List<Vuelo>)await _vueloService.GetAvailableFlightsByOrigin(vuelo.Origen);
            return Ok(vuelos);
        }
    }
}
