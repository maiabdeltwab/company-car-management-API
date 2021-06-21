using CarManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessCardController : ControllerBase
    {
        private readonly CarManageDBContext _context;

        public AccessCardController(CarManageDBContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Recharge(int id, [FromBody] double amount)
        {
            if (amount < 0)
            {
                return BadRequest();
            }

            var card = await _context.AccessCards.FindAsync(id);
            string message = "";

            if (card.Credit < 0)
            {
                message = $"=> {-1 * card.Credit}$ has been deducted from the card";
            }

            card.Credit += amount;
            _context.SaveChanges();

            message = $"The card has been charged and your balance is {card.Credit}$\n\n" + message;

            return Content(message);
        }
    }
}