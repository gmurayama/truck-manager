using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruckManager.Application.Features.Motoristas;

namespace TruckManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> RecuperarCadastroDoMotorista(
            [FromQuery] RecuperarCadastroDoMotorista.Query query,
            [FromServices] RecuperarCadastroDoMotorista.QueryHandler handler)
        {
            var motorista = await handler.Handle(query);

            if (motorista == null)
                return NoContent();

            return Ok(motorista);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarMotorista(
            [FromBody] CadastrarMotorista.Command command,
            [FromServices] CadastrarMotorista.CommandHandler handler)
        {
            var resolved = await handler.Handle(command);
            return resolved.Match<IActionResult>(
                Ok: () => Ok(),
                Err: (err) => BadRequest(err)
            );
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarCadastroDoMotorista(
            [FromBody] AtualizarCadastroDoMotorista.Command command,
            [FromServices] AtualizarCadastroDoMotorista.CommandHandler handler)
        {
            var resolved = await handler.Handle(command);
            return resolved.Match<IActionResult>(
                Ok: () => Ok(),
                Err: (err) => BadRequest(err)
            );
        }
    }
}