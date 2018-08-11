using BLL.DTO;
using System;
using System.Collections.Generic;
using Marvin.JsonPatch;


namespace BLL.Interfaces
{
    public interface ITaskService
    {
        TaskDTO AddTask(TaskDTO taskDTO);
        TaskDTO UpdateTask(int id, TaskDTO taskDTO);
        TaskDTO PatchTask(int id, JsonPatchDocument<TaskDTO> jsonPatchDocument);
        void DeleteTask(int id);
        TaskDTO GetTask(int? id);
        IEnumerable<TaskDTO> GetTasks(string sortOptions, string label);
        void Dispose();
    }
}
