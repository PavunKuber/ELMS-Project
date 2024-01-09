using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ELMSApplication;
using ELMSApplication.Filter;

namespace ELMSApplication.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly ELMSApplicationContext _context;

        public LeaveController(ELMSApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string? employeeId = HttpContext.Session.GetString("EmployeeID");
            string? admin = HttpContext.Session.GetString("Admin");

            if (admin != null && admin.Equals("False"))
            {
                var leave = _context.Leave.Where(l => l.EmployeeId.Equals(employeeId));
                return View(await leave.AsNoTracking().ToListAsync());
            }
            else if (admin != null && admin.Equals("True"))
            {
                var leave = _context.Leave.AsQueryable();
                return View(await leave.AsNoTracking().ToListAsync());
            }
            else
            {
                return Problem("Invalid admin value.");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> UserProfile(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveId,EmployeeId,StartDate,EndDate,LeaveType,Status")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(leave);
                    await _context.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveId,EmployeeId,StartDate,EndDate,LeaveType,Status")] Leave leave)
        {
            if (id != leave.LeaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.LeaveId))
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

            return View(leave);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.FirstOrDefaultAsync(m => m.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leave == null)
            {
                return Problem("Entity set 'ELMSApplicationContext.Leave' is null.");
            }

            var leave = await _context.Leave.FindAsync(id);
            if (leave != null)
            {
                _context.Leave.Remove(leave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approved(int? id)
        {
            var leave = await _context.Leave.FindAsync(id);
            leave.Status = "Approved";
            _context.Update(leave);
            await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RejectedAsync(int id)
        {
            var leave = await _context.Leave.FindAsync(id);
            leave.Status = "Rejected";
            _context.Update(leave);
            await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(int id)
        {
            return _context.Leave?.Any(e => e.LeaveId == id) ?? false;
        }
    }
}
