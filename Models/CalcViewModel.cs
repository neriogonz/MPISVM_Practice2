namespace WebTeploobmenApp.Models
{
    public class CalcViewModel : Formulas.Data
    {
        public double[,] ResultTable { get; set; }
        public List<double> LayerPositions { get; set; }
        public List<double> MaterialTemperatures { get; set; }
        public List<double> GasTemperatures { get; set; }
        public List<double> TemperatureDifferences { get; set; }
    }
}
