// Universidad.API/Endpoints/FacultadEndpoints.cs
using Universidad.Application.DTOs.Commands;
using Universidad.Application.Services;

namespace Universidad.API.Endpoints;

public static class FacultadEndpoints
{
    public static void MapFacultadEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/facultades");

        group.MapGet("/", async (IFacultadService service) =>
        {
            var facultades = await service.GetAllAsync();
            return Results.Ok(facultades);
        })
        .WithName("GetAllFacultades")
        .WithOpenApi();

        group.MapPost("/", async (CreateFacultadCommand command, IFacultadService service) =>
        {
            try
            {
                var result = await service.CreateFacultadAsync(command);
                return Results.Created($"/api/facultades/{result.FacultadId}", result);
            }
            catch (FacultadConflictException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
        })
        .WithName("CreateFacultad")
        .WithOpenApi();
    }
}
