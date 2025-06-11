using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class TaskUpdate
    {
        public int Id { get; set; }

        [Required]
        public string content { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }

        public TaskItem? TaskItem { get; set; }
    }
}
