using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Group/{groupId}/Students")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int groupId)
        {
            var users = await _studentService.Get(x => x.GroupId == groupId);
            users.ForEach(c => c.Password = null);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int groupId, int id)
        {
            var user = await _studentService.Get(id);
            user.Password = null;
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int groupId, [FromBody] Student model)
        {
            model.GroupId = groupId;
            model = await _studentService.Update(model);

            return base.Ok(model);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int groupId, long id, [FromBody] Student model)
        {
            model.GroupId = groupId;

            var user = await _studentService.Get(id);
            if (user == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(model.Password))
                model.Password = user.Password;

            model = await _studentService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int groupId, int id)
        {
            var user = await _studentService.Get(id);
            if (user == null || user.GroupId != groupId)
                return NotFound();

            await _studentService.Remove(id);

            return NoContent();
        }
    }
}