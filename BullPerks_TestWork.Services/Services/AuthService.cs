using Azure.Core;
using BullPerks_TestWork.Api.DB.IdentityModels;
using BullPerks_TestWork.DAL;
using BullPerks_TestWork.Domain.Constants;
using BullPerks_TestWork.Domain.Interfaces.Services;
using DiplomeProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly EFDbContext _context;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IJwtTokenService _IJwtTokenService;

        public AuthService(EFDbContext context, UserManager<DbUser> userManager, SignInManager<DbUser> signInManager, IJwtTokenService iJwtTokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _IJwtTokenService = iJwtTokenService;
        }

        public async Task<string> RegisterUser(LoginViewModel model)
        {
            var roleName = ProjectRoles.ADMIN; // We can only register admin
            var userRegister = _context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (userRegister != null)
            {
                return null;
            }

            var dbUser = new DbUser()
            {
                Id = Guid.NewGuid().ToString(),
                Login = model.Email,
                Email = model.Email,
                UserName = model.Email,
                RegisterDate = DateTime.Now
            };
            var result = await _userManager.CreateAsync(dbUser, model.Password);
            if (!result.Succeeded)
            {
                return null;
            }

            result = await _userManager.AddToRoleAsync(dbUser, roleName);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(dbUser, isPersistent: false);
                return await _IJwtTokenService.CreateToken(dbUser);
            }

            return null;
        }

        public async Task<string> LoginUser(LoginViewModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager
                .PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var token = await _IJwtTokenService.CreateToken(user);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return token;
        }
    }
}
