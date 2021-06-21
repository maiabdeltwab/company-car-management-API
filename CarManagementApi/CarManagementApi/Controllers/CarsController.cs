using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarManagementApi.Models;

namespace CarManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarManageDBContext _context;

        public CarsController(CarManageDBContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.Where(c => c.Id == id)
                            .Include(c => c.AccessCard)
                            .Include(c => c.Employee)
                            .FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            if (!CarExists(id))
            {
                return NotFound();
            }

            _context.Entry(car).State = EntityState.Modified;

            if (car.AccessCard != null)
            {
                _context.Entry(car.AccessCard).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return Content("Car updated successfully");
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            //create access card for the car (with welcome credit => 10$)
            car.AccessCard = new AccessCard();

            //insert car into DB
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            car.Employee = await _context.Employees.FindAsync(car.EmployeeId);

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            var accessCard = await _context.AccessCards.Where(c => c.Id == car.AccessCardId)
                                                       .FirstOrDefaultAsync();
            if (car == null)
            {
                return NotFound();
            }

            _context.AccessCards.Remove(accessCard);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Content("Car deleted successfully");
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}