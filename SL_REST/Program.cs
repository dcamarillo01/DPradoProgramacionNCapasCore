using DL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtener connection string 
var connectionString =
    builder.Configuration.GetConnectionString("DPradoProgramacionNCapas")
        ?? throw new InvalidOperationException("Connection string"
        + "'DPradoProgramacionNCapas' not found.");

//Hacer inyeccion de dependencia(connection string)
builder.Services.AddDbContext<DpradoProgramacionNcapasContext>(options =>
    options.UseSqlServer(connectionString));

//Donde va a vivir la coneccion.
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Login>();

//Configuracion de JWT
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
    });

builder.Services.AddAuthorization();

//Ignore DataAnotations
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
