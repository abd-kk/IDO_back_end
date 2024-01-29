namespace IDO.Models
{
    public class ToDoTaskDto
    {
        public int? toDoId { get; set; }
        public string toDoTitle { get; set; }
        public string toDoStatus { get; set; }
        public string toDoImportance { get; set; }
        public string toDoCategory { get; set; }
        public DateTime toDoDueDate { get; set; }
        public string toDoEstimate { get; set; }
    }
}
