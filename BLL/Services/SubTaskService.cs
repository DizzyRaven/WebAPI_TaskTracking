using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using Marvin.JsonPatch;

using BLL.DTO;
using DAL.Entities;
//using BLL.BusinessModels;
using DAL.Interfaces;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Repositories;
using BLL.Factories;

namespace BLL.Services
{
    public class SubTaskService : ISubTaskService
    {
        IUnitOfWork Database { get; set; }
        SubTaskFactory subTaskFactory = new SubTaskFactory();

        public SubTaskService(string connectionString)
        {
            Database = new EFUnitOfWork(connectionString);
        }
        //public SubTaskService(IUnitOfWork uow)
        //{
        //    Database = uow;
        //}
        public SubTaskDTO AddSubTask(SubTaskDTO subTaskDTO)
        {
            //Task task = Database.Tasks.Get(subTaskDTO.TaskId);

            //// Validation 
            //if (task == null)
            //{
            //    throw new ValidationException("Task does not exists!", "");
            //}
            SubTask subTask = subTaskFactory.CreateSubTask(subTaskDTO);

            var result = Database.SubTasks.Create(subTask);
            if (result.Status == DAL.RepositoryActionStatus.Created)
            {
                var newSubTask = subTaskFactory.CreateSubTask(result.Entity);
                return newSubTask;
            }
            else
                throw new ValidationException("", "");

        }
        public IEnumerable<SubTaskDTO> GetSubTasks(int TaskId)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubTask, SubTaskDTO>()).CreateMapper();

            //return mapper.Map<IEnumerable<SubTask>, List<SubTaskDTO>>(Database.SubTasks.GetAll());
            var subtasks = Database.SubTasks.GetAll(TaskId);
            if (subtasks == null)
                throw new ValidationException("Task with this Id does not exists", "");

            var result = subtasks.ToList().Select(s => subTaskFactory.CreateSubTask(s));

            return result;
        }
        public SubTaskDTO GetSubTask(int id, int? taskId)
        {
            SubTask subtask = null;
            if (taskId == null)
            {
                subtask = Database.SubTasks.Get(id);
            }
            else
            {
                var subTasksFromTaskId = Database.SubTasks.GetAll(taskId.Value);

                if(subTasksFromTaskId!=null)
                {
                    subtask = subTasksFromTaskId.FirstOrDefault(x => x.Id == id);
                }
            }
            if (subtask != null)
            {
                var result = subTaskFactory.CreateSubTask(subtask);
                return result;
            }
            else
            {
                throw new ValidationException("SubTask not found", "");
            }
        }
        public SubTaskDTO UpdateSubTask(SubTaskDTO subTaskDTO, int id)
        {
            SubTask subTask = subTaskFactory.CreateSubTask(subTaskDTO);

            var result = Database.SubTasks.Update(subTask);
            if(result.Status == DAL.RepositoryActionStatus.Updated)
            {
                var updatedSubTask = subTaskFactory.CreateSubTask(result.Entity);
                return updatedSubTask;
            }
            else if(result.Status == DAL.RepositoryActionStatus.NotFound)
            {
                throw new ValidationException("Subtask not found", "");
            }
            throw new DatabaseException("", "");
        }
        public SubTaskDTO PatchSubTask(int id,JsonPatchDocument<SubTaskDTO> jsonPatchDocument)
        {
            var subtask = Database.SubTasks.Get(id);

            if (subtask == null)
                throw new ValidationException("Subtask not found", "");

            //Creating DTO to apply json-patch
            var subTaskDTO = subTaskFactory.CreateSubTask(subtask);

            jsonPatchDocument.ApplyTo(subTaskDTO);

            // To update entity we should create SubTask object form patched DTO
            var result = Database.SubTasks.Update(subTaskFactory.CreateSubTask(subTaskDTO));
            if( result.Status == DAL.RepositoryActionStatus.Updated)
            {
                SubTaskDTO updatedSubTask = subTaskFactory.CreateSubTask(result.Entity);
                return updatedSubTask;
            }
            throw new DatabaseException("Somethig wrong on database side", "");
           
        }
        public void DeleteSubTask(int id)
        {
            var result = Database.SubTasks.Delete(id);
            if (result.Status == DAL.RepositoryActionStatus.Deleted)
            {
                return;
            }
            else if (result.Status == DAL.RepositoryActionStatus.NotFound)
            {
                throw new ValidationException("Subtask with id " + id.ToString() + " does not exists!", "");
            }
            throw new DatabaseException(    "","");
        }
        public void Dispose()
        {
            Database.Dispose();
        }

       
    }
}
