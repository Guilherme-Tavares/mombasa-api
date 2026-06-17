using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Produtor;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/produtores")]
    public class ProdutorController : ControllerBase
    {
        private readonly ProdutorService _service;

        public ProdutorController(ProdutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var produtores = await _service.FindAll();
                return Ok(produtores);
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
                var produtor = await _service.FindById(id);
                return Ok(produtor);
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
        public async Task<IActionResult> Create([FromBody] ProdutorCreateDto dto)
        {
            try
            {
                var produtor = await _service.Create(dto);
                return Created("", produtor);
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
        public async Task<IActionResult> Update(string id, [FromBody] ProdutorUpdateDto dto)
        {
            try
            {
                var produtor = await _service.Update(id, dto);
                return Ok(produtor);
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
}
