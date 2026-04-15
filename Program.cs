using Microsoft.EntityFrameworkCore;
using MinimalCrud.DATA;

var builer = WebApplication.CreateBuilder();

builer.Services.AddControllers();

builer.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

var app = builer.Build();

app.MapControllers();

app.Run();