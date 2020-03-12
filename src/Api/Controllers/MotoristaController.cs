using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruckManager.Application.Features.Motoristas;

namespace Api.Controllers
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

        
    }
}