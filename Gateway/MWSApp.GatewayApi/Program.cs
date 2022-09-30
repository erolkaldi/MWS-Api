using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();
builder.Configuration.AddJsonFile("configuration.json");

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder => {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseCors("AllowOrigin");
app.UseOcelot();

app.UseAuthorization();

app.MapControllers();

app.Run();
