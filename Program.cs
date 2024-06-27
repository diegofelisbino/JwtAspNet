using JwtAspNet.Models;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", (TokenService service) 
=>
{
    var usuario = new Usuario(
                1,
                "diego.felisbino@outlook.com",
                "Diego Felisbino",
                "123456",
                ["premium", "student"]);

    return service.Create(usuario);
});

app.Run();
