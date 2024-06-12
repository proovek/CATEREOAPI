using CatereoAPI.Data;
using CatereoAPI.Model;
using CatereoAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [ApiController]
    [Route("api/menuitems")]
    public class MenuItemController : ControllerBase
    {

        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAllMenuItems()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItems();
            return Ok(menuItems);
        }

        [Authorize]
        [HttpGet]
        [Route("menuitemsDayByDay")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAllMenuItemsDayByDay()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItemsDayByDay();
            return Ok(menuItems);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemRepository.GetMenuItemById(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<int>> AddMenuItem([FromBody] MenuItem menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest();
            }

            var addedItemId = await _menuItemRepository.AddMenuItem(menuItem);
            return Ok(addedItemId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateMenuItem(int id, [FromBody] MenuItem menuItem)
        {
            if (menuItem == null || id != menuItem.MenuItemId)
            {
                return BadRequest();
            }

            var updatedItemId = await _menuItemRepository.UpdateMenuItem(menuItem);
            return Ok(updatedItemId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteMenuItem(int id)
        {
            var deletedItemId = await _menuItemRepository.DeleteMenuItem(id);
            if (deletedItemId == -1)
            {
                return NotFound();
            }
            return Ok(deletedItemId);
        }
    }
}
