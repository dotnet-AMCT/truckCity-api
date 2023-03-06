namespace truckCity_api.Models.Dto
{
    public class TruckDto
    {
        public int Id { get; private set; }

        public string LicencePlate { get; set; } //(correct format);

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int? PlantId { get; set; }

        public Plant? CurrentPlant { get; set; } //foreign key to Plant

        public bool IsSold { get; set; }

        public List<PartName>? BrokenParts { get; set; }

        public List<PartCode>? CompatiblePartCodes { get; set; }

        public TruckDto(string brand, string licencePlate, string model)
        {
            Brand = brand;
            LicencePlate = licencePlate;
            Model = model;
        }
    }
}

