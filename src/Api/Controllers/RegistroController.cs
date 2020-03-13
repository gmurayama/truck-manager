using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckManager.Application.Features.Registros;

namespace TruckManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AdicionarNovoRegistro(
            [FromBody] RegistrarPassagemPeloTerminal.Command command,
            [FromServices] RegistrarPassagemPeloTerminal.CommandHandler handler)
        {
            await handler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarListaDeRegistrosDoMotorista(
            [FromQuery] RecuperarListaDeRegistrosDoMotorista.Query query,
            [FromServices] RecuperarListaDeRegistrosDoMotorista.QueryHandler handler)
        {
            var registros = await handler.Handle(query);

            if (registros.Count > 0)
                return Ok(registros);
            else
                return NoContent();
        }
    }
}