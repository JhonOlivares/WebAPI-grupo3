using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;
using WebAPIGrupo3.Services;

namespace WebAPIGrupo3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TuristaController : ControllerBase
    {
        private readonly ITuristaService _turistaService;
        public TuristaController(ITuristaService turistaService)
        {
            _turistaService = turistaService;
        }

        [HttpGet]
        public async Task<IEnumerable<Turista>> Get()
        {
            return await _turistaService.GetAllTuristas();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Turista>> Get(string id)
        {
            var turista = await _turistaService.GetTurista(id);
            if (turista == null)
            {
                return NotFound($"El turista: {id} no existe");
            }
            return turista;
        }

        [HttpPost]
        public async Task<ActionResult> PostClient([FromBody] Turista turista)
        {
            string text = await _turistaService.Create(turista);
            if (string.IsNullOrEmpty(text))
                return NotFound("400");
            else
                return Created("path", new { id = text });
        }
    }
}
