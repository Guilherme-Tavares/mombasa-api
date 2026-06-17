using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.Medicamento;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Authorize]
[Route("v{version:apiVersion}/medicamentos")]
public class MedicamentoController : ControllerBase
{
    private readonly MedicamentoService _service;

    public MedicamentoController(MedicamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var medicamentos = await _service.FindAll();
            return Ok(medicamentos);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> FindAllV2([FromQuery] MedicamentoFilter filter)
    {
        try
        {
            var medicamentos = await _service.FindAllV2(filter);
            return Ok(medicamentos);
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
            var medicamento = await _service.FindById(id);
            return Ok(medicamento);
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
    public async Task<IActionResult> Create([FromBody] MedicamentoCreateDto dto)
    {
        try
        {
            var medicamento = await _service.Create(dto);
            return Created("", medicamento);
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
    public async Task<IActionResult> Update(string id, [FromBody] MedicamentoUpdateDto dto)
    {
        try
        {
            var medicamento = await _service.Update(id, dto);
            return Ok(medicamento);
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