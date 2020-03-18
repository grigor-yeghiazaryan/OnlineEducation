using OnlineEducation.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Group/{groupId}/Students")]
    [Authorize(Roles = "Professor")]
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
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int groupId, int id)
        {
            var user = await _studentService.Get(id);
            if (user.GroupId != groupId)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int groupId, [FromBody] Student model)
        {
            model.GroupId = groupId;
            model = await _studentService.Add(model);

            return base.Ok(model);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(int groupId, int id, [FromBody] StudentChangePasswordModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
                return BadRequest();

            var student = await _studentService.Get(id);

            if (student == null || student.GroupId != groupId)
                return NotFound();

            var data = await _studentService.Update(student);

            return base.Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int groupId, int id, [FromBody] Student model)
        {
            var student = await _studentService.Get(id);

            if (student == null || student.GroupId != groupId)
                return NotFound();

            var result = await _studentService.Update(model);

            return base.Ok(result);
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