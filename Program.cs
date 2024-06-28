using JwtAspNet;
using JwtAspNet.Extensions;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    x.AddPolicy("Premium", policy => policy.RequireRole("premium"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (TokenService service) =>
{
    var usuario = new Usuario(
                1,
                "diego.felisbino@outlook.com",
                "Diego Felisbino",
                "123456",
                ["premium", "student"]);

    return service.Create(usuario);
});

app.MapGet("/restrito", (ClaimsPrincipal user) => new
{
    id = user.GetId(),
    email = user.GetEmail(),
    name = user.GetName(),
    givenName = user.GetGivenName()
})
    .RequireAuthorization();

app.MapGet("/admin", () => "Você tem acesso")
    .RequireAuthorization("Admin");

app.Run();
