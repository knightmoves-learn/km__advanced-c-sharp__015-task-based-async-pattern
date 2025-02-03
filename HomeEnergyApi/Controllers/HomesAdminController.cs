using Microsoft.AspNetCore.Mvc;
using HomeEnergyApi.Models;
using HomeEnergyApi.Services;
using System.Threading.Tasks;

namespace HomeEnergyApi.Controllers
{
    [ApiController]
    [Route("admin/Homes")]
    public class HomeAdminController : ControllerBase
    {
        private IWriteRepository<int, Home> repository;

        private ZipCodeLocationService zipCodeLocationService;

        public HomeAdminController(IWriteRepository<int, Home> repository, ZipCodeLocationService zipCodeLocationService)
        {
            this.repository = repository;
            this.zipCodeLocationService = zipCodeLocationService;
        }

        [HttpPost]
        public IActionResult CreateHome([FromBody] Home home)
        {
            repository.Save(home);
            return Created($"/Homes/{repository.Count() - 1}", home);
        }

        [HttpPost("Location/{zipCode}")]
        public IActionResult ZipLocation(int zipCode)
        {
            Place place = zipCodeLocationService.Report(zipCode);
            return Ok(place);
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