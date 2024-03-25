using BullPerks_TestWork.Api.DB.IdentityModels;
using BullPerks_TestWork.DAL;
using BullPerks_TestWork.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Services.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly EFDbContext _context;

        public JwtTokenService(UserManager<DbUser> userManager, EFDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> CreateToken(DbUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            roles = roles.OrderBy(x => x).ToList();
            var query = _context.Users.AsQueryable();


            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.Id),
                new Claim("login",user.Login),
            };
            foreach (var el in roles)
            {
                claims.Add(new Claim("roles", el));
            }

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a8f5f167f44f4964e6c998dee827110c"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(1),
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
