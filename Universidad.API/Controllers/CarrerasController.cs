using Microsoft.AspNetCore.Mvc;
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.Interfaces.Services;

namespace Universidad.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrerasController : ControllerBase
{
    private readonly ICarreraService _carreraService;

    public CarrerasController(ICarreraService carreraService)
    {
        _carreraService = carreraService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetCarreras()
    {
        try
        {
            var carreras = await _carreraService.GetAllCarrerasAsync();
            return Ok(carreras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetCarrera(int id)
    {
        try
        {
            var carrera = await _carreraService.GetCarreraByIdAsync(id);
            return Ok(carrera);
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

    [HttpGet("facultad/{facultadId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetCarrerasByFacultad(int facultadId)
    {
        try
        {
            var carreras = await _carreraService.GetCarrerasByFacultadAsync(facultadId);
            return Ok(carreras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<object>> CreateCarrera(CreateCarreraCommand command)
    {
        try
        {
            var carrera = await _carreraService.CreateCarreraAsync(command);
            return CreatedAtAction(nameof(GetCarrera), new { id = carrera.CarreraId }, carrera);
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

    [HttpPut("{id}")]
    public async Task<ActionResult<object>> UpdateCarrera(int id, UpdateCarreraCommand command)
    {
        try
        {
            var carrera = await _carreraService.UpdateCarreraAsync(id, command);
            return Ok(carrera);
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
    public async Task<ActionResult> DeleteCarrera(int id)
    {
        try
        {
            await _carreraService.DeleteCarreraAsync(id);
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

    [HttpPatch("{id}/activar")]
    public async Task<ActionResult> ActivarCarrera(int id)
    {
        try
        {
            await _carreraService.ActivateCarreraAsync(id);
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