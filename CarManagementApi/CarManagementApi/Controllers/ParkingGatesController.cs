using CarManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingGatesController : ControllerBase
    {
        private readonly CarManageDBContext _context;

        public ParkingGatesController(CarManageDBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Car>>> GetParkingCars(bool inParking = true)
        {
            var cars = new List<Car>();
            if (inParking)
            {
                cars = await _context.Cars.Where(c => c.IsInParking == true)
                           .Include(c => c.AccessCard)
                           .Include(c => c.Employee)
                           .ToListAsync();
            }
            else
            {
                cars = await _context.Cars.Where(c => c.IsInParking == false)
                           .Include(c => c.AccessCard)
                           .Include(c => c.Employee)
                           .ToListAsync();
            }

            return cars;
        }

        // GET: api/Cars
        [HttpPut]
        public async Task<ActionResult> Gate(int CarId, bool exitGate)
        {
            string message = "";

            if (exitGate)
            {
                var car = await _context.Cars.Where(c => c.Id == CarId)
                                             .Include("AccessCard")
                                             .FirstOrDefaultAsync();

                var currentDate = DateTime.Now;

                var lastAcess = car.AccessCard.LastOperation.AddMinutes(1);

                //check if the car passes through the highway gate 2 times within 1 minute
                if (lastAcess > currentDate)
                {
                    message = "Second pass is free";
                }
                else
                {
                    car.AccessCard.LastOperation = DateTime.Now;
                    car.AccessCard.Credit -= 4;

                    var balance = car.AccessCard.Credit;

                    //check if there is remaining balance in the card
                    if (balance < 0)
                    {
                        message = "You have used up your credit, please recharge the card\n" +
                                  $"You owe {balance}$";
                    }
                    else
                    {
                        message = $"remaining balance in the card => {balance}$";
                    }
                }

                car.IsInParking = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                var car = await _context.Cars.Where(c => c.Id == CarId)
                             .Include("Employee")
                             .FirstOrDefaultAsync();

                car.IsInParking = true;
                await _context.SaveChangesAsync();

                message = $"Employee {car.Employee.FirstName} {car.Employee.LastName}'s car enter the parking";
            }
            return Content(message);
        }
    }
}