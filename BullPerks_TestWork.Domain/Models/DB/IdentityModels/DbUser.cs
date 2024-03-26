using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BullPerks_TestWork.Domain.DB.IdentityModels
{
    public class DbUser : IdentityUser
    {
        public string Login { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
