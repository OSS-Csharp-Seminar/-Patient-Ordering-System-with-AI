using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrdersModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = (await _orderService.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _orderService.AddAsync(Order);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != Order.Id)
            {
                return BadRequest("Order ID mismatch.");
            }

            _orderService.Update(Order);
            await _orderService.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.Remove(order);
            await _orderService.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckAvailabilityAsync()
        {
            var availableTimeSlots = await _orderService.GetAvailableTimeSlotsAsync();
            // You can add code here to handle the available time slots, e.g., showing them in the UI
            return Page();
        }
    }
}
