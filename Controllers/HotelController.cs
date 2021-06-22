using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;
using WebAPIGrupo3.Services;

namespace WebAPIGrupo3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController( IHotelService hotelservice)
        {
            _hotelService = hotelservice;
        }
        
        
        [HttpGet]
        public async Task<IEnumerable<Hotel>> Get()
        {
            return await _hotelService.GetAllHotels();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> Get(string id)
        {
            var hotel = await _hotelService.GetHotel(id);
            if ( hotel == null)
            {
                return NotFound($"El hotel: {id} no existe.");
            }
            return hotel;
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    RPClientes rpCli = new RPClientes();

        //    var cliRet = rpCli.ObtenerCliente(id);

        //    if (cliRet == null)
        //    {
        //        var nf = NotFound("El cliente " + id.ToString() + " no existe.");
        //        return nf;
        //    }

        //    return Ok(cliRet);
        //}
    }

}
