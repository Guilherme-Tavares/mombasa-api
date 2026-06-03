using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Propriedade;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[Route("/propriedades")]
[ApiController]
public class PropriedadeController : ControllerBase
{
    private readonly PropriedadeService _service;

    public PropriedadeController(PropriedadeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var propriedades = await _service.FindAll();
            return Ok(propriedades);
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
            var propriedade = await _service.FindById(id);
            return Ok(propriedade);
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
    public async Task<IActionResult> Create([FromBody] PropriedadeCreateDto dto)
    {
        try
        {
            var propriedade = await _service.Create(dto);
            return Created("", propriedade);
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
    public async Task<IActionResult> Update(string id, [FromBody] PropriedadeUpdateDto dto)
    {
        try
        {
            var propriedade = await _service.Update(id, dto);
            return Ok(propriedade);
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
