using Azure.Core;
using BullPerks_TestWork.DAL;
using BullPerks_TestWork.Domain.Constants;
using BullPerks_TestWork.Domain.DB.IdentityModels;
using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Domain.ViewModels;
using DiplomeProject.ViewModels;
using Microsoft.AspNetCore.Identity;

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

        public async Task<string> RegisterUser(RegisterViewModel model)
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
                UserName = model.Name,
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
