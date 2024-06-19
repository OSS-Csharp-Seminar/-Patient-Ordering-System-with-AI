using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
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
            Order = new Order(); // Initialize the Order property
            Orders = new List<Order>(); // Initialize the Orders property
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
            if (Order == null) // Check if Order is null
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _orderService.AddAsync(Order);
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
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var orderToUpdate = await _orderService.GetByIdAsync(id);
            if (orderToUpdate == null)
            {
                return NotFound();
            }

            orderToUpdate.DoctorId = Order.DoctorId;
            orderToUpdate.PatientId = Order.PatientId;
            orderToUpdate.DateOfAppointment = Order.DateOfAppointment;
            orderToUpdate.Diagnosis = Order.Diagnosis;

            await _orderService.UpdateAsync(orderToUpdate);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckAvailabilityAsync()
        {
            return Page();
        }
    }
}
