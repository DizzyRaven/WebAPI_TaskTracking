using BLL.DTO;
using System;
using System.Collections.Generic;
using Marvin.JsonPatch;


namespace BLL.Interfaces
{
    public interface ISubTaskService
    {
        SubTaskDTO AddSubTask(SubTaskDTO subTaskDTO);
        void DeleteSubTask(int id);
        SubTaskDTO GetSubTask(int id, int? taskId);
        SubTaskDTO UpdateSubTask(SubTaskDTO subTask, int id);
        SubTaskDTO PatchSubTask(int id,JsonPatchDocument<SubTaskDTO> jsonPatchDocument);
        IEnumerable<SubTaskDTO> GetSubTasks(int TaskId);
        void Dispose();
    }
}
