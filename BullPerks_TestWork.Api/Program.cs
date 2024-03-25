using BullPerks_TestWork.Api.DB.IdentityModels;
using BullPerks_TestWork.Api.DB.Models;
using BullPerks_TestWork.Api.Repositories.Interfaces;
using BullPerks_TestWork.DAL;
using BullPerks_TestWork.DAL.Repositories;
using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
#region DB
builder.Services.AddDbContext<EFDbContext>(options =>
                    options.UseSqlServer(connectionString));

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a8f5f167f44f4964e6c998dee827110c"));
builder.Services.AddIdentity<DbUser, IdentityRole>(options =>
{
    options.Stores.MaxLengthForKeys = 256;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<EFDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signingKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // set ClockSkew is zero
        ClockSkew = TimeSpan.Zero
    };
});
#endregion

#region DI
builder.Services.AddTransient<IRepository<DbToken>, DbTokenRepository>();
builder.Services.AddTransient<ICryptoWalletService, CryptoWalletService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IAuthService, AuthService>();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
