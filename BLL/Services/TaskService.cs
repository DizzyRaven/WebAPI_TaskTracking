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
using BLL.Helpers;
using BLL.BusinessModels;

namespace BLL.Services
{
    public class TaskService : ITaskService
    {
        IUnitOfWork Database { get; set; }
        TaskFactory taskFactory = new TaskFactory();

        // Interaction with DAL using IUnitOfWork object
        public TaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<TaskDTO> GetTasks(string sort, string label)
        {
            int labelId = -1;
            if (label != null)
            {
                switch (label.ToLower())
                {
                    case "important": labelId = 1;
                        break;
                    case "frozen": labelId = 2;
                        break;
                    default:
                        break; 
                } 
            }

            var tasks = Database.Tasks.GetAll();

            return tasks
                .ApplySort(sort)
                .Where(t=>(labelId==-1||t.LabelId == labelId))
                .ToList()

                .Select(t => taskFactory.CreateTask(t));
        }

        public TaskDTO GetTask(int? id)

        {
            if (id == null)
                throw new ValidationException("Task id has not been set", "");
            Task task = Database.Tasks.Get(id.Value);
            if (task == null)
                throw new ValidationException("Task has not found", "");

            return taskFactory.CreateTask(task);
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO AddTask(TaskDTO taskDTO)
        {
            Task task = taskFactory.CreateTask(taskDTO);
            task.Progress = TaskProgress.GetTaskProgress(task);
            var result = Database.Tasks.Create(task);
            if (result.Status == DAL.RepositoryActionStatus.Created)
            {
                var newTask = taskFactory.CreateTask(result.Entity);
                return newTask;
            }
            throw new DatabaseException("Something wrong with data", "");
        }
        public TaskDTO UpdateTask(int id, TaskDTO taskDTO)
        {
            Task task = taskFactory.CreateTask(taskDTO);
            task.Progress = TaskProgress.GetTaskProgress(task);

            var result = Database.Tasks.Update(task);
            if (result.Status == DAL.RepositoryActionStatus.Updated)
            {
                //Database.Save();
                var updatedTask = taskFactory.CreateTask(result.Entity);
                return updatedTask;
            }
            else if (result.Status == DAL.RepositoryActionStatus.NotFound)
                throw new ValidationException("Task not found", "");
            else if (result.Status == DAL.RepositoryActionStatus.Error)
                throw new DatabaseException(result.Exception.Message, "");
            throw new Exception();

        }
        public TaskDTO PatchTask(int id, JsonPatchDocument<TaskDTO> taskPatchDocument)
        {
            var task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new ValidationException("Task not found", "");
            }

            // Map
            var taskDTO = taskFactory.CreateTask(task);

            // Applying patch document to dto 
            taskPatchDocument.ApplyTo(taskDTO);

            // Convertion patched dto to entity
            Task resTask = taskFactory.CreateTask(taskDTO);
            task.Progress = TaskProgress.GetTaskProgress(resTask);

            Database.Tasks.Update(resTask);
            Database.Save();

            return taskDTO;
        }
        void ITaskService.DeleteTask(int id)
        {
            var result = Database.Tasks.Delete(id);
            if (result.Status == DAL.RepositoryActionStatus.Deleted)
                return;
            if (result.Status == DAL.RepositoryActionStatus.NotFound)
                throw new ValidationException("Task not found", "");
            else if (result.Status == DAL.RepositoryActionStatus.Error)
                throw new DatabaseException(result.Exception.Message, "");
            throw new Exception();

        }
    }
}
