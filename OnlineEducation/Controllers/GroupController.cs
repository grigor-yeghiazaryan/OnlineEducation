using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Groups")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            this._groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _groupService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _groupService.Get(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Group model)
        {
            var data = await _groupService.Add(model);

            return Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] Group model)
        {
            var data = await _groupService.Get(id);
            if (data == null)
                return NotFound();

            model = await _groupService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _groupService.Get(id);
            if (data == null)
                return NotFound();

            await _groupService.Remove(id);

            return NoContent();
        }
    }
}