using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace truckCity_api.Models
{
    public class Part
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public string Name { get; set; }

        [
            Required,
            MaxLength(200),
            MinLength(1),
            RegularExpression(
                @"([A-Za-z]+([0-9]+[A-Za-z]+)+)", 
                ErrorMessage = "Use only numbers, lowercase and uppercase."
            ),
            Comment("The code to identify the part")
        ]
        public string Code { get; set; }

        public int? TruckId { get; set; }

        [Comment("The truck to repair where it's assigned")]
        public Truck? Truck { get; set; }

        public Part(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }

    /*public enum PartNames
    {
        Radiator,
        BrakeGroup,
        Engine,
        FrontFrame,
        Filters,
        FuelSystem,
        RearLights,
        FrontLights,
        NumberPlateLights,
        Clutch,
        Rim,
        RightDoor,
        LeftDoor,
        Windshield
    }

    public class PartName
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PartNames Name { get; set; }

        public PartName(PartNames name)
        {
            Name = name;
        }
    }

    public class PartCode
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(50), Comment("The code to identify the part")]
        public string Code { get; set; } = null!;

        public PartCode (string code)
        {
            Code = code;
        }
    }*/
}