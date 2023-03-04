using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml;

namespace truckCity_api.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; private set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public int CurrentCapacity { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; } //       (arg format)

        public Plant()
        {

        }
    }
}
