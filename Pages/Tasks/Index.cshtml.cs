using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
using TMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace TMS.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<TaskItem> TaskItem { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            int selectedUserId = 1;

            var tasks = _context.Tasks
                .Include(t => t.AppUser)
                .Where(t => t.AppUserId == selectedUserId);

            if (!string.IsNullOrEmpty(SearchString))
            {
                tasks = tasks.Where(t => t.Title!.Contains(SearchString));
            }

            TaskItem = await tasks.ToListAsync();
        }

    }
}