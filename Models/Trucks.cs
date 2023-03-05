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
        public int Id { get; private set; }

        [Required]
        public string LicencePlate { get; set; } //(correct format);

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        public string? CurrentPlant { get; set; } //foreign key to Plant

        [Required]
        public bool IsSold { get; set; }

        public List<PartName>? BrokenParts { get; set; }

        [Required]
        public List<PartCode>? CompatiblePartCodes { get; set; }

        public Truck(string brand, string licencePlate, string model)
        {
            Brand = brand;
            LicencePlate = licencePlate;
            Model = model;
        }
    }
}
