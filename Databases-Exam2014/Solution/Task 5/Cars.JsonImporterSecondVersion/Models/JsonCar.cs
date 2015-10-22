namespace Cars.JsonImporterSecondVersion.Models
{
    using Cars.Models;

    public class JsonCar
    {
        public ushort Year { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public string ManufacturerName { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public Dealer Dealer { get; set; }
    }
}
