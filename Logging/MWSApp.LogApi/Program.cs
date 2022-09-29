var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterServices();
builder.Services.RegisterDbContextServices(builder.Configuration);
var app = builder.Build();

app.Services.CreateScope().MigrateDatabase();
app.UseAuthorization();

app.MapControllers();

app.Run();
