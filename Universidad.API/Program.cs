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
        Description = "API para gestión universitaria - Facultades, Carreras y Cursos",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Desarrollo",
            Email = "dev@universidad.edu"
        }
    });
});

// Database
builder.Services.AddDbContext<UniversidadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversidadConnection")));

// Dependency Injection
builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();
builder.Services.AddScoped<IFacultadService, FacultadService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Universidad API v1");
        c.RoutePrefix = "swagger"; // Para acceder en /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Al final de Program.cs, antes de app.Run();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UniversidadDbContext>();
        // Aplicar migraciones automáticamente
        context.Database.Migrate();
        // Insertar datos de prueba
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar la base de datos.");
    }
}


app.Run();