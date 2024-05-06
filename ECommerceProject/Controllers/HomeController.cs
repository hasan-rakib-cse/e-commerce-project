using ECommerceProject.Data;
using ECommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext  _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //      public IActionResult Index()
        //      {
        //          var plist = _context.Product.Include(x => x.ProductImages).ToList();
        //          return View(plist);
        //}

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(x => x.ProductImages).Include(p => p.Configaration);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
