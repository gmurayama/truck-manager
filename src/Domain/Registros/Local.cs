namespace Truckmanager.Domain
{
    public class Local
    {
        public string Nome { get; set; }

        public Localizacao Localizacao { get; set; }
    }

    public class Localizacao
    {
        public string Type { get; set; }

        public double[] Coordinates { get; set; }
    }
}