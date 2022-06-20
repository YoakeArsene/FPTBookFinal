#nullable disable

using BookStore.Areas.Identity.Data;
using BookStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly UserContext _context;
        private readonly UserManager<AppUser> _userManager;
        private int _recordsPerPage = 6;

        public OrderDetailsController(UserContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Index(int id)
        {
            var userContext = _context.OrderDetail.Where(o => o.OrderId == id)
                .Include(o => o.Book).Include(o => o.Order)
                .Include(o => o.Order.User).Include(o => o.Book.Store);
            return View(await userContext.ToListAsync());
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Manager(int id)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            var userContext = _context.OrderDetail.Where(o => o.Order.UId == user.Id && o.OrderId == id)
                .Include(o => o.Book).Include(o => o.Order)
                .Include(o => o.Order.User).Include(o => o.Book.Store);
            return View(await userContext.ToListAsync());
        }
    }
}