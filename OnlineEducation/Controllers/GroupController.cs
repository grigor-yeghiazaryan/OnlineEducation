using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.Common;

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
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get()
        {
            var data = await _groupService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _groupService.Get(id);
            return Ok(data);
        }

        [HttpPost]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Add([FromBody] Group model)
        {
            var data = await _groupService.Add(model);

            return Ok(data);
        }

        [HttpPost("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Edit(int id, [FromBody] Group model)
        {
            var data = await _groupService.Get(id);
            if (data == null)
                return NotFound();

            model = await _groupService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
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