using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class SubTaskRepository : IRepository<SubTask>
    {
        private TaskContext db;

        public SubTaskRepository(TaskContext context)
        {
            this.db = context;
        }
        public RepositoryActionResult<SubTask> Create(SubTask subtask)
        {
            try
            {
                db.SubTasks.Add(subtask);
                var result = db.SaveChanges();
                if(result> 0)
                {
                    return new RepositoryActionResult<SubTask>(subtask, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<SubTask>(subtask, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<SubTask>(null, RepositoryActionStatus.Error, ex);
            }
        }


        public IEnumerable<SubTask> Find(Func<SubTask, bool> predicate)
        {
            return db.SubTasks.Include(x => x.Task).Where(predicate).ToList();
        }

        public SubTask Get(int Id)
        {
            return db.SubTasks.Find(Id);
        }
        // DOTO read about include
        public IQueryable<SubTask> GetAll()
        {
            return db.SubTasks;
        }
        public IQueryable<SubTask> GetAll(int taskId)
        {
            Task task = db.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                return db.SubTasks.Where(s => s.TaskId == taskId);
            }
            return null;
        }
      

        public RepositoryActionResult<SubTask> Update(SubTask subTask)
        {
            // db.Entry(subTask).State = EntityState.Modified;

            try
            {
                var existingSubTask = db.SubTasks.FirstOrDefault(e => e.Id == subTask.Id);

                if(existingSubTask == null)
                {
                    return new RepositoryActionResult<SubTask>(subTask, RepositoryActionStatus.NotFound);
                }
                db.Entry(existingSubTask).State = EntityState.Detached;

                db.SubTasks.Attach(subTask);

                db.Entry(subTask).State = EntityState.Modified;

                var result = db.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<SubTask>(subTask, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<SubTask>(subTask, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<SubTask>(null, RepositoryActionStatus.Error, e);
            }
        }

        RepositoryActionResult<SubTask> IRepository<SubTask>.Delete(int id)
        {
            try
            {
                var subtask = db.SubTasks.Where(s => s.Id == id).FirstOrDefault();
                if(subtask!=null)
                {
                    db.SubTasks.Remove(subtask);
                    db.SaveChanges();
                    return new RepositoryActionResult<SubTask>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<SubTask>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<SubTask>(null, RepositoryActionStatus.Error, e);
            }
        }
    }
}
