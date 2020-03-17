using System.Linq;
using OnlineEducation.DTO;
using System.Threading.Tasks;
using OnlineEducation.Common;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OnlineEducation.Controllers
{
    [Produces("application/json")]
    [Route("api/Groups/{groupId}/Items")]
    [Authorize(Roles = "Professor")]
    public class GroupItemController : Controller
    {
        private readonly IItemGroupService _itemGroupService;
        private readonly IGroupService _groupService;
        private readonly IItemService _itemService;

        public GroupItemController(IItemGroupService itemGroupService, IGroupService groupService, IItemService itemService)
        {
            this._itemGroupService = itemGroupService;
            this._groupService = groupService;
            this._itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int groupId)
        {
            var data = await _itemGroupService.Get(x => x.GroupId == groupId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int groupId, [FromBody] AddItemsInGroupModel model)
        {
            if (model == null)
                return BadRequest();

            var group = await _groupService.Get(groupId);
            if (group == null)
                return NotFound();

            var item = await _itemService.Get(model.ItemId);
            if (item == null)
                return NotFound();

            var itemGroup = new ItemGroup { GroupId = groupId, ItemId = model.ItemId, DependencyType = model.DependencyType };
            var data = await _itemGroupService.Add(itemGroup);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int groupId, int id)
        {
            var data = await _itemGroupService.Get(x => x.GroupId == groupId && x.ItemId == id);
            var itemGroup = data.FirstOrDefault();
            if (itemGroup == null)
                return NotFound();

            await _itemGroupService.Remove(itemGroup.Id);

            return NoContent();
        }
    }
}