using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TMS.Models;

namespace TMS.Pages_Tasks
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public TaskItem? TaskItem { get; set; }

        [BindProperty]
        public TaskUpdate NewUpdate { get; set; } = new();

        public List<TaskUpdate> TaskUpdates { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            TaskItem = await _context.Tasks
                .Include(t => t.Updates)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (TaskItem == null)
                return NotFound();

            TaskUpdates = TaskItem.Updates
                .OrderByDescending(u => u.Timestamp)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddUpdateAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _context.Tasks
                .Include(t => t.Updates)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return NotFound();

            NewUpdate.Timestamp = DateTime.Now;
            NewUpdate.TaskItemId = task.Id;

            _context.TaskUpdates.Add(NewUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = task.Id });
        }
    }
}
