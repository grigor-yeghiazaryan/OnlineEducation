using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _studentService.GetAll();
            users.ForEach(c => c.Password = null);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _studentService.Get(id);
            user.Password = null;
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Student model)
        {
            model = await _studentService.Update(model);

            return base.Ok(model);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] Student model)
        {
            var user = await _studentService.Get(id);
            if (user == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(model.Password))
                model.Password = user.Password;

            model = await _studentService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _studentService.Get(id);
            if (user == null)
                return NotFound();

            await _studentService.Remove(id);

            return NoContent();
        }
    }
}