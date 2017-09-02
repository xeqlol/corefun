using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoreFun.Data;
using CoreFun.Models;

namespace CoreFun.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();

            var student = await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentDate,FirstMidName,LastName")] Student student)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.SingleOrDefaultAsync(e => e.ID == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.SingleOrDefaultAsync(e => e.ID == id);

            if(await TryUpdateModelAsync<Student>(student, "", s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }

            return View(student);
        }

        public async Task<IActionResult> Delete (int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
                return NotFound();

            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
                return RedirectToAction("Index");

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }
    }
}
