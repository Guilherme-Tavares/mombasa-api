using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.AbastecimentoCocho;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("v{version:apiVersion}/abastecimentos-cocho")]
public class AbastecimentoCochoController : ControllerBase
{
    private readonly AbastecimentoCochoService _service;

    public AbastecimentoCochoController(AbastecimentoCochoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        try { return Ok(await _service.FindAll()); }
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
    public async Task<IActionResult> Create([FromBody] AbastecimentoCochoCreateDto dto)
    {
        try { return Created("", await _service.Create(dto)); }
        catch (ServiceException e) { return e.ToActionResult(this); }
        catch (Exception e) { return Problem(e.Message); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] AbastecimentoCochoUpdateDto dto)
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
