using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TMS.Models;

namespace TMS.Pages_Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public TaskItem TaskItem { get; set; } = default!;
        public List<TaskUpdate> TaskUpdates { get; set; } = new();

        [BindProperty]
        public TaskUpdate NewUpdate { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            TaskItem? taskitem = await _context.Tasks
                .Include(t => t.Updates)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (taskitem == null)
                return NotFound();

            TaskItem = taskitem;
            TaskUpdates = taskitem.Updates
                .OrderByDescending(u => u.Timestamp)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddUpdateAsync(int? id)
        {
            if (id == null || string.IsNullOrWhiteSpace(NewUpdate.content))
                return RedirectToPage(new { id });

            NewUpdate.TaskItemId = id.Value;
            NewUpdate.Timestamp = DateTime.Now;

            _context.TaskUpdates.Add(NewUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id }); // Refresh to show new update
        }
    }
}
