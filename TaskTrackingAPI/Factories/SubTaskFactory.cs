using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;
using TaskTrackingAPI.Models;

namespace TaskTrackingAPI.Factories
{
    public class SubTaskFactory
    {
        public SubTaskFactory()
        {

        }
        public SubTaskDTO CreateSubTask(SubTaskViewModel subTaskViewModel)
        {
            return new SubTaskDTO()
            {
                Id = subTaskViewModel.Id,
                Name = subTaskViewModel.Name,
                Description = subTaskViewModel.Description,
                IsDone = subTaskViewModel.IsDone
            };
        }

        public SubTaskViewModel CreateSubTask(SubTaskDTO subTaskDto)
        {
            return new SubTaskViewModel()
            {
                Id = subTaskDto.Id,
                Name = subTaskDto.Name,
                Description = subTaskDto.Description,
                IsDone = subTaskDto.IsDone
            };
        }
    }
}