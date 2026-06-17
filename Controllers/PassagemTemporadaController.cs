using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.PassagemTemporada;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Authorize]
[Route("v{version:apiVersion}/passagens-temporada")]
public class PassagemTemporadaController : ControllerBase
{
    private readonly PassagemTemporadaService _service;

    public PassagemTemporadaController(PassagemTemporadaService service)
    {
        _service = service;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> FindAll()
    {
        try { return Ok(await _service.FindAll()); }
        catch (Exception e) { return Problem(e.Message); }
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> FindAllV2([FromQuery] PassagemTemporadaFilter filter)
    {
        try { return Ok(await _service.FindAllV2(filter)); }
        catch (Exception e) { return Problem(e.Message); }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(string id)
    {
        try { return Ok(await _service.FindById(id)); }
        catch (ServiceException e) { return e.ToActionResult(this); }
        catch (Exception e) { return Problem(e.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PassagemTemporadaCreateDto dto)
    {
        try { return Created("", await _service.Create(dto)); }
        catch (ServiceException e) { return e.ToActionResult(this); }
        catch (Exception e) { return Problem(e.Message); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] PassagemTemporadaUpdateDto dto)
    {
        try { return Ok(await _service.Update(id, dto)); }
        catch (ServiceException e) { return e.ToActionResult(this); }
        catch (Exception e) { return Problem(e.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        try { await _service.Remove(id); return NoContent(); }
        catch (ServiceException e) { return e.ToActionResult(this); }
        catch (Exception e) { return Problem(e.Message); }
    }
}