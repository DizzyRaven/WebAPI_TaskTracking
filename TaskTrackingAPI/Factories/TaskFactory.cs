using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTrackingAPI.Models;
using BLL.DTO;

namespace TaskTrackingAPI.Factories
{
    public class TaskFactory
    {
        public TaskDTO CreateTask(TaskViewModel taskViewModel)
        {
            return new TaskDTO
            {
                Id = taskViewModel.Id,
                Name = taskViewModel.Name,
                Description = taskViewModel.Description,
                Progress = taskViewModel.Progress,
                StartDate = taskViewModel.StartDate,
                DeadLine = taskViewModel.DeadLine,
                TaskLabelId = taskViewModel.TaskLabelId
            };
        }
        public TaskViewModel CreateTask(TaskDTO taskDTO)
        {
            return new TaskViewModel
            {
                Id = taskDTO.Id,
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                Progress = taskDTO.Progress,
                StartDate = taskDTO.StartDate,
                DeadLine = taskDTO.DeadLine,
                TaskLabelId = taskDTO.TaskLabelId
            };
        }
    }
}