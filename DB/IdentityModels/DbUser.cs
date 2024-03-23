using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BullPerks_TestWork.DB.IdentityModels
{
    public class DbUser : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Login { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
