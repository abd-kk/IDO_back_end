using System.ComponentModel.DataAnnotations;

namespace IDO.Models
{
    public class TaskImportance
    {
        [Key]
        public int taskImportanceId { get; set; }

        public string taskImportanceName { get; set; }
    }
}
