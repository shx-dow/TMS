using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool IsComplete { get; set; }

        public PriorityLevel Priority { get; set; }
    }
}
