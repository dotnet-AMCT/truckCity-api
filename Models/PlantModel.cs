using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;
using System.Xml;

namespace truckCity_api.Models
{
    public class Plant
    {
        [Key]
        public Guid Id { get; private set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        [Required]
        public uint MaxCapacity { get; set; }

        public uint CurrentCapacity { get; set; } = 0;

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; } = null!;    //(arg format)

        public List<Truck>? Trucks { get; set; }
    }
}