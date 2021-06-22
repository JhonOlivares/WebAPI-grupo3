using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;
using WebAPIGrupo3.Services;

namespace WebAPIGrupo3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalService _sucursalService;
        public SucursalController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet]
        public async Task<IEnumerable<Sucursal>> Get()
        {
            return await _sucursalService.GetAllAgencies();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> Get(string id)
        {
            var sucursal = await _sucursalService.GetAgency(id);
            if (sucursal == null)
            {
                return NotFound($"La sucursal: {id} no existe");
            }
            return sucursal;
        }
    }
}
