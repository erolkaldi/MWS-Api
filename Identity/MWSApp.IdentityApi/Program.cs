

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterDbContextServices(builder.Configuration);
var app = builder.Build();

app.Services.CreateScope().MigrateDatabase();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
