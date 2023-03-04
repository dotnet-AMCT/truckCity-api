using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace truckCity_api.Models
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public List<Part>? Parts { get; set; }
    }

    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, MaxLength(50), Comment("The code to identify the part")]
        public string Code { get; set; }

        [Comment("The truck to repair where it's assigned")]
        public int? TruckId { get; set; }

        public Truck? Truck { get; set; }

        public Part(string name, string code, int? truckId)
        {
            Name = name;
            Code = code;
            TruckId = truckId;
        }
    }
}