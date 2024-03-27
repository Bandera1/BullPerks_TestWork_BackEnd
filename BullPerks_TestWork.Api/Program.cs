using BullPerks_TestWork.Domain.DB.IdentityModels;
using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.Models.JSON;
using BullPerks_TestWork.Api.Repositories.Interfaces;
using BullPerks_TestWork.DAL;
using BullPerks_TestWork.DAL.Repositories;
using BullPerks_TestWork.Domain.Interfaces.Services;
using BullPerks_TestWork.Domain.ViewModels;
using BullPerks_TestWork.Services.Converters;
using BullPerks_TestWork.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nelibur.ObjectMapper;
using System.ComponentModel;
using System.Reflection;
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
builder.Services.AddTransient<IBLPTokenService, BLPTokenService>();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer",
                      new OpenApiSecurityScheme
                      {
                          Description = "JWT Authorization header using the Bearer scheme.",
                          Type = SecuritySchemeType.Http,
                          Scheme = "bearer"
                      });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
            new List<string>()
                    }
                });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        option.IncludeXmlComments(xmlPath);
    }
});
#endregion

// By the way, TinyMapper 8 times faster than AutoMapper. Details: http://tinymapper.net/
#region TinyMapper
TypeDescriptor.AddAttributes(typeof(CoinStatsGetWalletBalanceModel), new TypeConverterAttribute(typeof(DbTokenConverter)));

TinyMapper.Bind<CoinStatsGetWalletBalanceModel, DbToken>();
TinyMapper.Bind<DbToken, TokenViewModel>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
