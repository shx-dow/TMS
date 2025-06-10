using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public class TaskUpdate
    {
        public int Id { get; set; }

        [Required]
        public string? Note { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Foreign key to TaskItem
        public int TaskItemId { get; set; }

        [ForeignKey("TaskItemId")]
        public TaskItem? TaskItem { get; set; }

        // Optional: User name or role who added the update
        public string? UpdatedBy { get; set; }
    }
}
