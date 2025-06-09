using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TaskManager.Models;

namespace TMS.Pages_Tasks
{
    public class IndexModel : PageModel
    {
        private readonly TMS.Data.AppDbContext _context;

        public IndexModel(TMS.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<TaskItem> TaskItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TaskItem = await _context.Tasks.ToListAsync();
        }
    }
}
