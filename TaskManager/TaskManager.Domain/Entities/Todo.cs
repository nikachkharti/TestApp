using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    public class Todo
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;

        [Required]
        public Guid UserId { get; set; }
    }
}
