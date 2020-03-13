namespace TruckManager.Application.Features.Registros
{
    public partial class RecuperarListaDeRegistrosDoMotorista
    {
        public class Query
        {
            public int? Page { get; set; }

            public int? PageSize { get; set; }

            public string MotoristaId { get; set; }
        }
    }
}
