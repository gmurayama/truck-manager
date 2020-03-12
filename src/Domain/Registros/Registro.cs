using System;

namespace Truckmanager.Domain
{
    public class Registro
    {
        public string Id { get; set; }

        public string MotoristaId { get; set; }

        public bool EstaCarregado { get; set; }

        public Local Origem { get; set; }

        public Local Destino { get; set; }

        public DateTime Data { get; set; }
    }
}
