using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RFID.SimpleTask.Infrastructure.Persistence;

namespace WebMVC.Pages
{
    public class ListOrdersForSalesmanModel : PageModel
    {
        private readonly IApplicationDbContext _context;

        public ListOrdersForSalesmanModel(IApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(o => !o.IsDeleted)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateOrderAsync(Guid orderId, [FromForm] Dictionary<Guid, Order> orders)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var orderToUpdate = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (orderToUpdate == null)
            {
                return NotFound();
            }

            var updatedOrder = orders[orderId];

            foreach (var item in updatedOrder.OrderItems)
            {
                var existingItem = orderToUpdate.OrderItems.FirstOrDefault(oi => oi.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.ProductName = item.ProductName;
                    existingItem.Quantity = item.Quantity;
                    existingItem.Price = item.Price;
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteOrderAsync(Guid orderId)
        {
            var orderToDelete = await _context.Orders.FindAsync(orderId);

            if (orderToDelete == null)
            {
                return NotFound();
            }

            orderToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
