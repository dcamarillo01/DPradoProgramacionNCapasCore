using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Obtener connection string 
var connectionString =
    builder.Configuration.GetConnectionString("DPradoProgramacionNCapas")
        ?? throw new InvalidOperationException("Connection string"
        + "'DPradoProgramacionNCapas' not found.");

//Obtener endpoint 


//Hacer inyeccion de dependencia(connection string)
builder.Services.AddDbContext<DpradoProgramacionNcapasContext>(options =>
    options.UseSqlServer(connectionString));

//Hacer inyeccion de dependencia de APIENDPOINT
var config = builder.Configuration;
string apiKey = config["APIENDPOINT"];
var apiEndPoint = builder.Configuration.GetConnectionString("APIENDPOINT") ?? throw new InvalidOperationException("Connection string"
        + "'APIENDPOINT' not found.");

//Donde va a vivir la coneccion.
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Colonia>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Estado>();


var app = builder.Build();

//Obtener ENDPOINT
app.MapGet("/", () => $"API Key: {apiKey}, Connection String: {apiEndPoint}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
