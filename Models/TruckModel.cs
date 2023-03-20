using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;

namespace truckCity_api.Models
{
    public class Truck
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public string LicencePlate { get; set; } = null!; //(correct format);

        [Required]
        public string Brand { get; set; } = null!;

        [Required]
        public string Model { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        public Guid? PlantId { get; set; }

        public Plant? Plant { get; set; } //foreign key to Plant

        [Required]
        public bool IsSold { get; set; }

        public List<string>? BrokenParts { get; set; } = null;

        [Required]
        public List<string>? CompatiblePartCodes { get; set; } = null;
    }
}
