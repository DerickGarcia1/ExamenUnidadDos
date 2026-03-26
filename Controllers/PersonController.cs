using ExamenUnidadDos.Dtos.Persons;
using ExamenUnidadDos.Services.Persons;
using Microsoft.AspNetCore.Mvc;

namespace ExamenUnidadDos.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _personService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _personService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PersonCreateDto dto)
        {
            var result = await _personService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PersonEditDto dto)
        {
            var result = await _personService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _personService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}