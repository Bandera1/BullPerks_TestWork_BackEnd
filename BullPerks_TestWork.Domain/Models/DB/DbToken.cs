using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BullPerks_TestWork.Api.DB.Models
{
    public class DbToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public uint TotalSupply { get; set; }
        public uint CirculatingSupply { get; set; }
    }
}
