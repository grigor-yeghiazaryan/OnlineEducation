using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.Common;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Items")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            this._itemService = itemService;
        }

        [HttpGet]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get()
        {
            var data = await _itemService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _itemService.Get(id);
            return Ok(data);
        }

        [HttpPost]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Add([FromBody] Item model)
        {
            var data = await _itemService.Add(model);

            return Ok(data);
        }

        [HttpPost("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Edit(int id, [FromBody] Item model)
        {
            var data = await _itemService.Get(id);
            if (data == null)
                return NotFound();

            model = await _itemService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
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