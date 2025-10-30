using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.Interfaces.Services;

namespace Universidad.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultadesController : ControllerBase
{
    private readonly IFacultadService _facultadService;

    public FacultadesController(IFacultadService facultadService)
    {
        _facultadService = facultadService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetFacultades()
    {
        try
        {
            var facultades = await _facultadService.GetAllFacultadesAsync();
            return Ok(facultades);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetFacultad(int id)
    {
        try
        {
            var facultad = await _facultadService.GetFacultadByIdAsync(id);
            return Ok(facultad);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<object>> CreateFacultad(CreateFacultadCommand command)
    {
        try
        {
            var facultad = await _facultadService.CreateFacultadAsync(command);
            return CreatedAtAction(nameof(GetFacultad), new { id = facultad.FacultadId }, facultad);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<object>> UpdateFacultad(int id, UpdateFacultadCommand command)
    {
        try
        {
            var facultad = await _facultadService.UpdateFacultadAsync(id, command);
            return Ok(facultad);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFacultad(int id)
    {
        try
        {
            await _facultadService.DeleteFacultadAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}