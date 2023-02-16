using JWTAuthentication.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWTAuthentication.Models.DB_Cabinet;
using JWTAuthentication.Models.DB_User;
using JWTAuthentication.Models.DB_Saraban;
using JWTAuthentication.Models.DB_Doccir;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// For Entity Framework
builder.Services.AddDbContext<JwtDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("JwtDatabase")));
builder.Services.AddDbContext<RwCabinetContext>(options => options.UseSqlServer(configuration.GetConnectionString("CabinetDatabase")));
builder.Services.AddDbContext<RwUserContext>(options => options.UseSqlServer(configuration.GetConnectionString("UserDatabase")));
builder.Services.AddDbContext<RwSaraban64Context>(options => options.UseSqlServer(configuration.GetConnectionString("SarabanDatabase")));
builder.Services.AddDbContext<AotDoccirContext>(options => options.UseSqlServer(configuration.GetConnectionString("DoccirDatabase")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<JwtDBContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = true;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidIssuer = configuration["JWT:ValidIssuer"],
         ValidAudience = configuration["JWT:ValidAudience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
     };
 });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
