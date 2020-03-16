using System.Collections.Generic;
using Truckmanager.Domain;

namespace TruckManager.Application.Features.Registros
{
    public partial class ListarOrigensDestinos
    {
        public class ResponseModel
        {
            public List<string> Origens { get; set; }

            public List<string> Destinos { get; set; }
        }
    }
}
