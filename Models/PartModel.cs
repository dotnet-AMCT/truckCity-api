using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using static NuGet.Packaging.PackagingConstants;
using System.Runtime.Intrinsics.Arm;

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

        public Guid? TruckId { get; set; }

        [Comment("The truck to repair where it's assigned")]
        public Truck? Truck { get; set; } = null;

        public Part(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}