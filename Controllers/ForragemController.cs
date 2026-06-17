using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Forragem;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/forragens")]
public class ForragemController : ControllerBase
{
    private readonly ForragemService _service;

    public ForragemController(ForragemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var forragens = await _service.FindAll();
            return Ok(forragens);
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
            var forragem = await _service.FindById(id);
            return Ok(forragem);
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
    public async Task<IActionResult> Create([FromBody] ForragemCreateDto dto)
    {
        try
        {
            var forragem = await _service.Create(dto);
            return Created("", forragem);
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
    public async Task<IActionResult> Update(string id, [FromBody] ForragemUpdateDto dto)
    {
        try
        {
            var forragem = await _service.Update(id, dto);
            return Ok(forragem);
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
