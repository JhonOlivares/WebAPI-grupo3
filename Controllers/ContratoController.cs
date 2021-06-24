using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;
using WebAPIGrupo3.Services;

namespace WebAPIGrupo3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;
        public ContratoController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Contrato>> Get()
        //{
        //    return await _contratoService.GetAllContratos();
        //}



        [HttpGet("{id}")]
        public async Task<IEnumerable<Contrato>> Get(string id)
        {
            return await _contratoService.GetContratos(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostContrato([FromBody] ContratoAnonimo jsonObject)
        {
            Contrato contrato = await _contratoService?.Create(jsonObject);
            if (contrato == null)
                return BadRequest("400");
            else
                return Created("path", contrato);
        }
    }
}
