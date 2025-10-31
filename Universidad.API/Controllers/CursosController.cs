using Microsoft.AspNetCore.Mvc;
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.Interfaces.Services;

namespace Universidad.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursosController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetCursos()
    {
        try
        {
            var cursos = await _cursoService.GetAllCursosAsync();
            return Ok(cursos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetCurso(int id)
    {
        try
        {
            var curso = await _cursoService.GetCursoByIdAsync(id);
            return Ok(curso);
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

    [HttpGet("codigo/{codigo}")]
    public async Task<ActionResult<object>> GetCursoByCodigo(string codigo)
    {
        try
        {
            var curso = await _cursoService.GetCursoByCodigoAsync(codigo);
            return Ok(curso);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("carrera/{carreraId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetCursosByCarrera(int carreraId)
    {
        try
        {
            var cursos = await _cursoService.GetCursosByCarreraAsync(carreraId);
            return Ok(cursos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("nivel/{nivelSemestre}")]
    public async Task<ActionResult<IEnumerable<object>>> GetCursosByNivelSemestre(int nivelSemestre)
    {
        try
        {
            var cursos = await _cursoService.GetCursosByNivelSemestreAsync(nivelSemestre);
            return Ok(cursos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<object>> CreateCurso(CreateCursoCommand command)
    {
        try
        {
            var curso = await _cursoService.CreateCursoAsync(command);
            return CreatedAtAction(nameof(GetCurso), new { id = curso.CursoId }, curso);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<object>> UpdateCurso(int id, UpdateCursoCommand command)
    {
        try
        {
            var curso = await _cursoService.UpdateCursoAsync(id, command);
            return Ok(curso);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCurso(int id)
    {
        try
        {
            await _cursoService.DeleteCursoAsync(id);
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
    public async Task<ActionResult> ActivarCurso(int id)
    {
        try
        {
            await _cursoService.ActivateCursoAsync(id);
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