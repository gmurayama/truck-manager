using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruckManager.Application.Features.Registros;

namespace Api.Controllers
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
    }
}