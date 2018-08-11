using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    [Serializable]
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DeadLine { get; set; }

        public int? TaskLabelId { get; set; }
        public ICollection<SubTaskDTO> SubTasks { get; set; }
    }
}
