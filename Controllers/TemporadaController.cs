using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Temporada;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/temporadas")]
public class TemporadaController : ControllerBase
{
    private readonly TemporadaService _service;

    public TemporadaController(TemporadaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var temporadas = await _service.FindAll();
            return Ok(temporadas);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(string id)
    {
        try
        {
            var temporada = await _service.FindById(id);
            return Ok(temporada);
        }
        catch (ServiceException e)
        {
            return e.ToActionResult(this);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TemporadaCreateDto dto)
    {
        try
        {
            var temporada = await _service.Create(dto);
            return Created("", temporada);
        }
        catch (ServiceException e)
        {
            return e.ToActionResult(this);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TemporadaUpdateDto dto)
    {
        try
        {
            var temporada = await _service.Update(id, dto);
            return Ok(temporada);
        }
        catch (ServiceException e)
        {
            return e.ToActionResult(this);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        try
        {
            await _service.Remove(id);
            return NoContent();
        }
        catch (ServiceException e)
        {
            return e.ToActionResult(this);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
