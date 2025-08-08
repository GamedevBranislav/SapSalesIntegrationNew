using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SapSalesIntegrationNew.Data;
using SapSalesIntegrationNew.Models;

namespace SapSalesIntegrationNew
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesOrder()
        {
            var orders = await _context.SalesOrders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesOrder(int id)
        {
            var order = await _context.SalesOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesOrder(SalesOrder order)
        {
            _context.SalesOrders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesOrder), new { id = order.Id }, order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesOrder(int id, SalesOrder updatedOrder)
        {
            if (id != updatedOrder.Id)
                return BadRequest();

            _context.Entry(updatedOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.SalesOrders.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            var order = await _context.SalesOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.SalesOrders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
