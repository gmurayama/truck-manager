namespace Truckmanager.Domain
{
    public class Motorista
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public Sexo Sexo { get; set; }

        public int Idade { get; set; }

        public string Cpf { get; set; }

        public TipoCnh TipoCnh { get; set; }
    }
}
