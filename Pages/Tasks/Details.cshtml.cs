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
        private readonly TMS.Data.AppDbContext _context;

        public DetailsModel(TMS.Data.AppDbContext context)
        {
            _context = context;
        }

        public TaskItem TaskItem { get; set; } = default!;

        public List<TaskUpdate> TaskUpdates { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskItem? taskitem = await _context.Tasks
                .Include(t => t.Updates)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (taskitem is not null)
            {
                TaskItem = taskitem;

                // 👇 Assign Updates list
                TaskUpdates = taskitem.Updates.OrderByDescending(u => u.Timestamp).ToList();

                return Page();
            }

            return NotFound();
        }
    }
}
