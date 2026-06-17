using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Rebanho;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/rebanhos")]
public class RebanhoController : ControllerBase
{
    private readonly RebanhoService _service;

    public RebanhoController(RebanhoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var rebanhos = await _service.FindAll();
            return Ok(rebanhos);
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
            var rebanho = await _service.FindById(id);
            return Ok(rebanho);
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
    public async Task<IActionResult> Create([FromBody] RebanhoCreateDto dto)
    {
        try
        {
            var rebanho = await _service.Create(dto);
            return Created("", rebanho);
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
    public async Task<IActionResult> Update(string id, [FromBody] RebanhoUpdateDto dto)
    {
        try
        {
            var rebanho = await _service.Update(id, dto);
            return Ok(rebanho);
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
