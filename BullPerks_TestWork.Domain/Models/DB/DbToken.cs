using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BullPerks_TestWork.Domain.DB.Models
{
    public class DbToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public float? TotalSupply { get; set; }
        public float? CirculatingSupply { get; set; }
        public string? ContractAddress { get; set; }
    }
}
