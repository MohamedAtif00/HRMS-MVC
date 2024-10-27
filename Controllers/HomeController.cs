using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly HRMSDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, HRMSDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var departments = _context.Departments
                .Include(d => d.Employees)
                .Select(d => new DepartmentViewModel
                {
                    Name = d.Name,
                    EmployeeCount = d.Employees.Count()
                })
                .ToList();

            ViewBag.EmployeeCount = _context.Employees.Count();
            ViewBag.DepartmentCount =  _context.Departments.Count();
            ViewBag.PositionCount = _context.Positions.Count();

            return View(departments);
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
