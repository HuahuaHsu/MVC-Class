using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		// GET: /Home/Index
		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		//// GET: /Home/Index
		//[HttpGet]
		//public IActionResult Index(int n)
		//{
		//	return View();
		//}


		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult Fetch()
		{
			return View();
		}

		public IActionResult CheckName()
		{
			return View();
		}

		public IActionResult Employees()
		{
			return View();
		}

		public IActionResult EmployeeTable()
		{
			return View();
		}
	}
}
