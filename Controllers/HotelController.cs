using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;
using WebAPIGrupo3.Services;
using System;

namespace WebAPIGrupo3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelservice)
        {
            _hotelService = hotelservice;
        }


        [HttpGet]
        public async Task<IEnumerable<Hotel>> Get()
        {
            return await _hotelService.GetAvailableHotels();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> Get(string id)
        {
            var hotel = await _hotelService.GetHotel(id);
            if (hotel == null)
            {
                return NotFound($"El hotel: {id} no existe.");
            }
            return hotel;
        }

        [HttpGet("ciudad/{ciudad}")]
        public async Task<ActionResult<Hotel>> Getporciudad(string ciudad)
        {
            if (string.IsNullOrEmpty(ciudad?.Trim()))
                return BadRequest("400");

            List<Hotel> hoteles = (List<Hotel>)await _hotelService.GetAvailableHotelsByCity(ciudad);
            return Ok(hoteles);
        }


        [HttpGet("por-ciudad")]
        public async Task<ActionResult> GetByCity([FromBody] Hotel hotel)
        {
            if (string.IsNullOrEmpty(hotel.CiudadHotel?.Trim()))
                return BadRequest("400");

            List<Hotel> hoteles = (List<Hotel>)await _hotelService.GetAvailableHotelsByCity(hotel.CiudadHotel);
            return Ok(hoteles);
        }

    }

}
