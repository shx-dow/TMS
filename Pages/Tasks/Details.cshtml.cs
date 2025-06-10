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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskitem = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);

            if (taskitem is not null)
            {
                TaskItem = taskitem;

                return Page();
            }

            return NotFound();
        }
    }
}
