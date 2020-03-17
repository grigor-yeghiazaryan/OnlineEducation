using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.Common;
using Microsoft.AspNetCore.Authorization;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Items")]
    [Authorize(Roles = "Professor")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            this._itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _itemService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _itemService.Get(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Item model)
        {
            var data = await _itemService.Add(model);

            return Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Item model)
        {
            var data = await _itemService.Get(id);
            if (data == null)
                return NotFound();

            model = await _itemService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _itemService.Get(id);
            if (data == null)
                return NotFound();

            await _itemService.Remove(id);

            return NoContent();
        }
    }
}