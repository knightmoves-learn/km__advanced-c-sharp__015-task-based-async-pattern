using Microsoft.AspNetCore.Mvc;
using HomeEnergyApi.Models;
using HomeEnergyApi.Services;

namespace HomeEnergyApi.Controllers
{
    [ApiController]
    [Route("admin/Homes")]
    public class HomeAdminController : ControllerBase
    {
        private IWriteRepository<int, Home> repository;

        public HomeAdminController(IWriteRepository<int, Home> repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult CreateHome([FromBody] Home home)
        {
            repository.Save(home);
            return Created($"/Homes/{repository.Count() - 1}", home);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHome([FromBody] Home newHome, [FromRoute] int id)
        {
            if (id > (repository.Count() - 1))
            {
                return NotFound();
            }
            repository.Update(id, newHome);
            return Ok(newHome);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHome(int id)
        {
            if (id > (repository.Count() - 1))
            {
                return NotFound();
            }
            var home = repository.RemoveById(id);
            return Ok(home);
        }
    }
}