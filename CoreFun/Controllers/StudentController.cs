using Microsoft.AspNetCore.Mvc;
using CoreFun.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoreFun.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
    }
}
