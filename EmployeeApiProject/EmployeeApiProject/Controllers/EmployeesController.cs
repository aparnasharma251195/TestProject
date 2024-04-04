using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApiProject.Data;
using EmployeeApiProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApiProject.Controllers
{
    [Route("employees")] // Add route prefix to avoid repetition
    public class EmployeesController : Controller
    {
        private readonly EmployeeApiProjectContext _context;

        public EmployeesController(EmployeeApiProjectContext context)
        {
            _context = context;
        }

        // GET: /employees
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: /employees/details/5
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: /employees/create
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /employees/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create([Bind("ID,Name,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: /employees/edit/5
        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: /employees/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: /employees/delete/5
        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: /employees/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.ID == id);
        }
    }
}