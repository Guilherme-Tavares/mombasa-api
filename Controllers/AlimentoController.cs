using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Alimento;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[Route("/alimentos")]
[ApiController]
public class AlimentoController : ControllerBase
{
    private readonly AlimentoService _service;

    public AlimentoController(AlimentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var alimentos = await _service.FindAll();
            return Ok(alimentos);
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
            var alimento = await _service.FindById(id);
            return Ok(alimento);
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
    public async Task<IActionResult> Create([FromBody] AlimentoCreateDto dto)
    {
        try
        {
            var alimento = await _service.Create(dto);
            return Created("", alimento);
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
    public async Task<IActionResult> Update(string id, [FromBody] AlimentoUpdateDto dto)
    {
        try
        {
            var alimento = await _service.Update(id, dto);
            return Ok(alimento);
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
