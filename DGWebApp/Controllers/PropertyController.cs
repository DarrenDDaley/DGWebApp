using System;
using System.Linq;
using System.Threading.Tasks;
using DGWebApp.Models.Post;
using DGWebApp.Models.Put;
using DGWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DGWebApp.Controllers
{
    [Route("[controller]")]
    public class PropertyController : Controller
    {
        private IProperyRepositroy repository;

        public PropertyController(IProperyRepositroy repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var property = await repository.Select(id);

            if (property == null) {
                return NotFound();
            }

            return Ok(property);
        }

        [HttpGet("address/{address}")]
        public async Task<IActionResult> AddressGet(string address)
        {
            var properties = await repository.SelectAddress(address);

            if (properties == null || !properties.Any()) {
                return NotFound();
            }

            return Ok(properties);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]PostProperty property)
        {
            var id = await repository.Insert(property);

            if (id == 0) {
                return BadRequest("post request failed");
            }

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PutProperty property)
        {
            var result = await repository.Update(id, property);

            if (!result){
                return BadRequest("record not updated");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await repository.Delete(id);

            if (!result) {
                return BadRequest("record not deleted");
            }

            return Ok();
        }
    }
}