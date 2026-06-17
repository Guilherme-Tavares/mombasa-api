using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.Cocho;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Authorize]
[Route("v{version:apiVersion}/cochos")]
public class CochoController : ControllerBase
{
    private readonly CochoService _service;

    public CochoController(CochoService service)
    {
        _service = service;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var cochos = await _service.FindAll();
            return Ok(cochos);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> FindAllV2([FromQuery] CochoFilter filter)
    {
        try
        {
            var cochos = await _service.FindAllV2(filter);
            return Ok(cochos);
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
            var cocho = await _service.FindById(id);
            return Ok(cocho);
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
    public async Task<IActionResult> Create([FromBody] CochoCreateDto dto)
    {
        try
        {
            var cocho = await _service.Create(dto);
            return Created("", cocho);
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
    public async Task<IActionResult> Update(string id, [FromBody] CochoUpdateDto dto)
    {
        try
        {
            var cocho = await _service.Update(id, dto);
            return Ok(cocho);
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