using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Items/{itemId}/Lessons")]
    public class ItemsLessonController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsLessonController(IItemService itemService)
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
            var data = await _itemService.Update(model);

            return Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] Item model)
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