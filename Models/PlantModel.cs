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
        public int Id { get; private set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public int CurrentCapacity { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; } //       (arg format)

        public List<Truck>? Trucks { get; set; }

        public Plant(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}