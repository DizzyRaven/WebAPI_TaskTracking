using System.Linq;
using BLL.DTO;
using DAL.Entities;

namespace BLL.BusinessModels
{
    public static class TaskProgress
    {       
       static public float GetTaskProgress(Task task)
        {
            var subtasks = task.SubTasks;
            float progress = 0;

            foreach (var subtask in subtasks)
            {
                if (subtask.IsDone)
                    progress += 100 / subtasks.Count();
            }
            return progress;
        }
    }
}
