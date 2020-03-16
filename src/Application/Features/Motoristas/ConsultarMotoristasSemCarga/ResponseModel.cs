using System.Collections.Generic;
using Truckmanager.Domain;

namespace TruckManager.Application.Features.Motoristas
{
    public class ResponseModel
    {
        public Motorista Motorista { get; set; }

        public IEnumerable<Registro> Registros { get; set; }
    }
}
