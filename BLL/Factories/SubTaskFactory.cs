using BLL.DTO;
using DAL.Entities;
using System;

namespace BLL.Factories
{
   public class SubTaskFactory
    {
        public SubTaskFactory()
        {

        }
        public SubTaskDTO CreateSubTask(SubTask subTask)
        {
            return new SubTaskDTO()
            {
                Id = subTask.Id,
                Name = subTask.Name,
                Description = subTask.Description,
                IsDone = subTask.IsDone,
                TaskId = subTask.TaskId
            };
        }

        public SubTask CreateSubTask(SubTaskDTO subTaskDto)
        {
            return new SubTask()
            {
                Id = subTaskDto.Id,
                Name = subTaskDto.Name,
                Description = subTaskDto.Description,
                IsDone = subTaskDto.IsDone,
                TaskId = subTaskDto.TaskId
            };
        }
    }
}
