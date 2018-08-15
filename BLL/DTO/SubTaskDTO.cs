

namespace BLL.DTO
{
    public class SubTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }

        public int TaskId { get; set; }
    }
}
