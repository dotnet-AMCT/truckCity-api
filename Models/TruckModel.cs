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
        public Guid Id { get; set; }

        [Required]
        public string LicencePlate { get; set; } //(correct format);

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public uint Year { get; set; }

        [Required]
        public uint Kilometres { get; set; }

        public Plant? Plant { get; set; } = null; //foreign key to Plant

        [Required]
        public bool IsSold { get; set; }

        public List<string>? BrokenParts { get; set; } = null;

        public List<string>? CompatiblePartCodes { get; set; } = null;

        //public Truck(string brand, string licencePlate, string model, List<string>? brokenParts, List<string>? compatiblePartCodes)
        //{
        //    Brand = brand;
        //    LicencePlate = licencePlate;
        //    Model = model;
        //    BrokenParts = brokenParts;
        //    CompatiblePartCodes = compatiblePartCodes;
        //}
    }
}
