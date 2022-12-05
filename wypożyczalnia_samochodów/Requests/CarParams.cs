namespace CarRent.Requests
{
    public class CarParams
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public float HorsePowerFrom { get; set; }
        public float HorsePowerTo { get; set; }
        public float PriceFrom { get; set; }
        public float PriceTo { get; set; }
        public float CombustionFrom { get; set; }
        public float CombustionTo { get; set; }
        public string Localization { get; set; }
        public string PriceSort { get; set; }

    }
}
