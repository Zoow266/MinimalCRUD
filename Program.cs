using Microsoft.EntityFrameworkCore;
using MinimalCrud.DATA;
using MinimalCrud.Services;

var builer = WebApplication.CreateBuilder();

builer.Services.AddScoped<IUserService, UserService>();

builer.Services.AddControllers();

builer.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

var app = builer.Build();

app.MapControllers();

app.Run();