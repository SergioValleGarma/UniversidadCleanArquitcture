using Microsoft.EntityFrameworkCore;
using Universidad.Application.Interfaces.Services;
using Universidad.Application.Services;
using Universidad.Domain.Interfaces;
using Universidad.Infrastructure.Data;
using Universidad.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Universidad API",
        Version = "v1",
        Description = "API para gestión universitaria"
    });
});

// Database
builder.Services.AddDbContext<UniversidadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversidadConnection")));

// Dependency Injection
builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();
builder.Services.AddScoped<IFacultadService, FacultadService>();

// AutoMapper - CORREGIR ESTA LÍNEA
builder.Services.AddAutoMapper(typeof(Universidad.Application.Mappings.FacultadProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Universidad API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Data seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UniversidadDbContext>();
        // Los datos ya fueron insertados por la migración
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error durante la inicialización de la base de datos.");
    }
}

app.Run();