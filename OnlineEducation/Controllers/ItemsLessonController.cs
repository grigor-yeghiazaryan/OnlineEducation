﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.Common;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Items/{itemId}/Lessons")]
    public class ItemsLessonController : Controller
    {
        private readonly IItemLessonService _itemLessonService;

        public ItemsLessonController(IItemLessonService itemLessonService)
        {
            this._itemLessonService = itemLessonService;
        }

        [HttpGet]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get(int itemId)
        {
            var data = await _itemLessonService.Get(x => x.ItemId == itemId);
            return Ok(data);
        }

        [HttpGet("{id}")]
        [AuthorizeUser(ClaimType.Student, ClaimType.Professor)]
        public async Task<IActionResult> Get(int itemId, int id)
        {
            var data = await _itemLessonService.Get(id);
            return Ok(data);
        }

        [HttpPost]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Add(int itemId, [FromBody] ItemLesson model)
        {
            model.ItemId = itemId;
            var data = await _itemLessonService.Add(model);

            return Ok(data);
        }

        [HttpPost("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Edit(int itemId, int id, [FromBody] ItemLesson model)
        {
            var data = await _itemLessonService.Get(id);
            if (data == null || data.ItemId != itemId)
                return NotFound();

            model = await _itemLessonService.Update(model);

            return base.Ok(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeUser(ClaimType.Professor)]
        public async Task<IActionResult> Remove(int itemId, int id)
        {
            var data = await _itemLessonService.Get(id);
            if (data == null || data.ItemId != itemId)
                return NotFound();

            await _itemLessonService.Remove(id);

            return NoContent();
        }
    }
}