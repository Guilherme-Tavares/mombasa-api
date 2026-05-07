using Microsoft.AspNetCore.Mvc;
using MombasaAPI.Dtos.Bovino;
using MombasaAPI.Exceptions;
using MombasaAPI.Services;

namespace MombasaAPI.Controllers
{
    [Route("/bovinos")]
    [ApiController]
    public class BovinoController : ControllerBase
    {
        private readonly BovinoService _service;

        public BovinoController(BovinoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var bovinos = await _service.FindAll();
                return Ok(bovinos);
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
                var bovino = await _service.FindById(id);
                return Ok(bovino);
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
        public async Task<IActionResult> Create([FromBody] BovinoCreateDto dto)
        {
            try
            {
                var bovino = await _service.Create(dto);
                return Created("", bovino);
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
        public async Task<IActionResult> Update(string id, [FromBody] BovinoUpdateDto dto)
        {
            try
            {
                var bovino = await _service.Update(id, dto);
                return Ok(bovino);
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
