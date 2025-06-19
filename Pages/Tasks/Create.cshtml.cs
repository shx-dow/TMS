using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TMS.Models;

namespace TMS.Pages_Tasks
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = default!;

        public List<SelectListItem> Users { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _context.AppUsers
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Users = await _context.AppUsers
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Name
                    }).ToListAsync();

                return Page();
            }

            _context.Tasks.Add(TaskItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
