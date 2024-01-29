using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDO.Models
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }

        public string taskTitle { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]

        public ToDoTaskStatus Status { get; set; }

        public string taskCategory { get; set; }

        public DateTime taskDueDate { get; set; }

        public string taskEstimate { get; set; }

        public int taskImportanceId { get; set; } 

        [ForeignKey("taskImportanceId")] 
        public TaskImportance TaskImportance { get; set; }

        public int userId { get; set; }

        [ForeignKey("userId")]

        public AppUser user { get; set; }
    }
}
