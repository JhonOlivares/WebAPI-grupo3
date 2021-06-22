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
            return await _vueloService.GetAllFlights();
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
    }
}
