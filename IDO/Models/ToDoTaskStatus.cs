using System.ComponentModel.DataAnnotations;

namespace IDO.Models
{
    public class ToDoTaskStatus
    {
        [Key]
        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
