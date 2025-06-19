using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<TaskItem>? Tasks { get; set; }
    }
}