namespace CarRent.Requests
{
    public class CarParams
    {
        public int id { get; set; }
        public string Class { get; set; }
        public string color { get; set; }
        public string name { get; set; }
        public float horsePowerFrom { get; set; }
        public float horsePowerTo { get; set; }
        public float priceFrom { get; set; }
        public float priceTo { get; set; }
        public float combustionFrom { get; set; }
        public float combustionTo { get; set; }
        public string localization { get; set; }
        public string priceSort { get; set; }

    }
}
