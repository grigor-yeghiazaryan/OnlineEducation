using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/StudentInfo")]
    [Authorize(Roles = "Student")]
    public class StudentInfoController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentInfoController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var someClaim = claimsIdentity.FindFirst("StudentId");

            int.TryParse(someClaim.Value, out int studentId);

            var userInfo = await _studentService.GetStudentInfo(studentId);
            return Ok(userInfo);
        }

        [HttpGet("GetLessans")]
        public async Task<IActionResult> GetLessans()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var someClaim = claimsIdentity.FindFirst("StudentId");

            int.TryParse(someClaim.Value, out int studentId);

            var userInfo = await _studentService.GetLessans(studentId);
            return Ok(userInfo);
        }
    }
}