using DiplomeProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> LoginUser(LoginViewModel model);
    }
}
