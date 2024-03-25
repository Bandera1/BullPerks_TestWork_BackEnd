using BullPerks_TestWork.Api.DB.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Domain.Interfaces.Services
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(DbUser user);
    }
}
