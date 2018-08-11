namespace DAL.Entities
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}