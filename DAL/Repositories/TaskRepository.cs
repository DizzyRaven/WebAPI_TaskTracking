using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private TaskContext db;

        public TaskRepository(TaskContext context)
        {
            this.db = context;
        }

        public IQueryable<Task> GetAll()
        {
            return db.Tasks;
        }
        public IQueryable<Task> GetAll(int id)
        {
            return db.Tasks;
        }
        public Task Get(int id)
        {
            return db.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public RepositoryActionResult<Task> Create(Task task)
        {
            try
            {
                db.Tasks.Add(task);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Task>(task, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Task>(task, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<Task>(null, RepositoryActionStatus.Error, e);
            }
        }
        public RepositoryActionResult<Task> Update(Task task)
        {
            try
            {
                var existingTask = db.Tasks.FirstOrDefault(tsk => tsk.Id == task.Id);

                if (existingTask == null)
                {
                    return new RepositoryActionResult<Task>(task, RepositoryActionStatus.NotFound);
                }

                db.Entry(existingTask).State = EntityState.Detached;

                db.Tasks.Attach(task);

                db.Entry(task).State = EntityState.Modified;

                var result = db.SaveChanges();

                if (result > 0)
                {
                    return new RepositoryActionResult<Task>(task, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Task>(task, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<Task>(task, RepositoryActionStatus.Error, e);
            }
        }
        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return db.Tasks.Where(predicate).ToList();
        }
        public RepositoryActionResult<Task> Delete(int id)
        {
            try
            {
                Task task = db.Tasks.Find(id);
                if (task != null)
                {
                    // EF would remove all linked subtasks

                    db.Tasks.Remove(task);
                    db.SaveChanges();
                    return new RepositoryActionResult<Task>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Task>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<Task>(null, RepositoryActionStatus.Error, e);
            }
        }
    }
}
