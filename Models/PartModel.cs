using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace truckCity_api.Models
{
    public enum PartNames
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
    }

    public class Part
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public PartNames Name { get; set; }

        public int CodeId { get; set; }

        [Required, Comment("The code to identify the part")]
        public PartCode Code { get; set; } = null!;

        public int? TruckId { get; set; }

        [Comment("The truck to repair where it's assigned")]
        public Truck? Truck { get; set; }

        public Part(PartNames name, int codeId, int? truckId)
        {
            Name = name;
            CodeId = codeId;
            TruckId = truckId;
        }
    }
}