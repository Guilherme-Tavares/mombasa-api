using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Divisao;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[Route("/divisoes")]
[ApiController]
public class DivisaoController : ControllerBase
{
    private readonly DivisaoService _service;

    public DivisaoController(DivisaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var divisoes = await _service.FindAll();
            return Ok(divisoes);
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
            var divisao = await _service.FindById(id);
            return Ok(divisao);
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
    public async Task<IActionResult> Create([FromBody] DivisaoCreateDto dto)
    {
        try
        {
            var divisao = await _service.Create(dto);
            return Created("", divisao);
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
    public async Task<IActionResult> Update(string id, [FromBody] DivisaoUpdateDto dto)
    {
        try
        {
            var divisao = await _service.Update(id, dto);
            return Ok(divisao);
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
