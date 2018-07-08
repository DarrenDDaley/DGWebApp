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
    public class LandlordController : Controller
    {
        private readonly ILandlordRepository repository;

        public LandlordController(ILandlordRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var landlord = await repository.Select(id);

            if(landlord == null) {
                return NotFound();
            }
            
            return Ok(landlord);
        }

        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> PhoneGet(string phone)
        {
            var landlords = await repository.PhoneSelect(phone);

            if (landlords == null || !landlords.Any()) {
                return NotFound();
            }

            return Ok(landlords);
        }

        [HttpGet("{id}/property")]
        public async Task<IActionResult> PropertyGet(int id)
        {
            var landlords = await repository.PropertySelect(id);

            if (landlords == null || !landlords.Any()) {
                return NotFound();
            }

            return Ok(landlords);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> EmailGet(string email)
        {
            var landlords = await repository.EmailSelect(email);

            if (landlords == null || !landlords.Any()) {
                return NotFound();
            }

            return Ok(landlords);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]PostLandlord landlord)
        {
            var id = await repository.Insert(landlord);

            if(id == 0) {
                return BadRequest("post request failed");
            }

            return Ok(id);
        }


        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]PutLandlord landlord)
        {
            var result = await repository.Update(id, landlord);

            if (!result) {
                return BadRequest("record not updated");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await repository.Delete(id);

            if(!result) {
                return BadRequest("record not deleted");
            }

            return Ok();
        }
    }
}