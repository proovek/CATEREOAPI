using CatereoAPI.Data;
using CatereoAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{

    [ApiController]
    [Route("api/menucards")]
    public class MenuCardController : ControllerBase
    {
        private readonly IMenuCardRepository _menuCardRepository;

        public MenuCardController(IMenuCardRepository menuCardRepository)
        {
            _menuCardRepository = menuCardRepository ?? throw new ArgumentNullException(nameof(menuCardRepository));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCard>>> GetAllMenuCards()
        {
            var menuCards = await _menuCardRepository.GetAllMenuCards();
            return Ok(menuCards);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuCard>> GetMenuCardById(int id)
        {
            var menuCard = await _menuCardRepository.GetMenuCardById(id);
            if (menuCard == null)
            {
                return NotFound();
            }
            return Ok(menuCard);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<int>> AddMenuCard([FromBody] MenuCard menuCard)
        {
            if (menuCard == null)
            {
                return BadRequest();
            }

            var addedMenuCardId = await _menuCardRepository.AddMenuCard(menuCard);
            return Ok(addedMenuCardId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<int>> UpdateMenuCard(int id, [FromBody] MenuCard updatedMenuCard)
        {
            if (updatedMenuCard == null || id != updatedMenuCard.MenuCardId)
            {
                return BadRequest();
            }

            var updatedMenuCardId = await _menuCardRepository.UpdateMenuCard(updatedMenuCard);
            return Ok(updatedMenuCardId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteMenuCard(int id)
        {
            var deletedMenuCardId = await _menuCardRepository.DeleteMenuCard(id);
            if (deletedMenuCardId == -1)
            {
                return NotFound();
            }
            return Ok(deletedMenuCardId);
        }
    }
}
