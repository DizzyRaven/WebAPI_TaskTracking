
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Factories
{
    public class TaskFactory
    {
        // TOD: fix mappers
        SubTaskFactory subTaskFactory = new SubTaskFactory();

        public TaskFactory()
        {

        }
        public TaskDTO CreateTask(Task task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Progress = task.Progress,
                StartDate = task.StartDate,
                DeadLine = task.DeadLine,
                TaskLabelId = task.LabelId,
                SubTasks = task.SubTasks.Select(e => subTaskFactory.CreateSubTask(e)).ToList()
            };
        }
        public Task CreateTask(TaskDTO taskDTO)
        {
            return new Task
            {
                Id = taskDTO.Id,
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                Progress = taskDTO.Progress,
                StartDate = taskDTO.StartDate,
                DeadLine = taskDTO.DeadLine,
                CreationDate = DateTime.Now,
                LabelId = taskDTO.TaskLabelId,
                SubTasks = taskDTO.SubTasks == null ? new List<SubTask>() : taskDTO.SubTasks.Select(subTask => subTaskFactory.CreateSubTask(subTask)).ToList()
            };
        }
       
    }
}
