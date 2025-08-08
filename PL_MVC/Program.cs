using DL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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


//Donde va a vivir la coneccion.
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Colonia>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Estado>();
builder.Services.AddScoped<BL.Empleado>();
builder.Services.AddScoped<BL.Departamento>();
builder.Services.AddScoped<BL.UserProfile>();
builder.Services.AddScoped<BL.Permiso>();
builder.Services.AddScoped<BL.HistorialPermiso>();

//Ignore DataAnnotations

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//Sessions in .Net Core
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Ensures the session cookie is accessible only by the server
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

// To use injeccion of Sessions
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d4c9482eb6bab9aef587ff82afcb000d"))
        };

        options.Events = new JwtBearerEvents { 
            
            OnMessageReceived = context => 
            {
                if (context.Request.Cookies.ContainsKey("session"))
                {
                    context.Token = context.Request.Cookies["session"];
                }

                return Task.CompletedTask;
            },
            OnChallenge = context => //401
            {
                context.HandleResponse();
                context.Response.Redirect("/Login/Login");

                return  Task.CompletedTask; 
            },
            OnForbidden = context => 
            {

                context.Response.Redirect("/Home/AccessDenied");
                return Task.CompletedTask;
            }
        };

    });



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//Use session
app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");


app.Run();
