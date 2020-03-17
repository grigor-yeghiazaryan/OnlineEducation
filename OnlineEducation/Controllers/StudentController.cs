﻿using OnlineEducation.DTO;
using System.Threading.Tasks;
using OnlineEducation.Common;
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
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get(int groupId)
        {
            var users = await _studentService.Get(x => x.GroupId == groupId);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get(int groupId, int id)
        {
            var user = await _studentService.Get(id);
            if (user.GroupId != groupId)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Add(int groupId, [FromBody] StudentModel model)
        {
            model.GroupId = groupId;
            model = await _studentService.Add(model);

            return base.Ok(model);
        }

        [HttpPost("ChangePassword")]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> ChangePassword(int groupId, int id, [FromBody] StudentChangePasswordModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
                return BadRequest();

            var student = await _studentService.Get(id);

            if (student == null || student.GroupId != groupId)
                return NotFound();

            student.Password = model.NewPassword;

            var data = await _studentService.Update(student);

            return base.Ok(data);
        }

        [HttpPost("{id}")]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Edit(int groupId, int id, [FromBody] StudentModel model)
        {
            var student = await _studentService.Get(id);

            if (student == null || student.GroupId != groupId)
                return NotFound();

            model = await _studentService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
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