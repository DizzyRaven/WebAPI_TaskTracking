using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    class TaskLabelRepository : IRepository<TaskLabel>
    {
        private TaskContext db;

        public TaskLabelRepository(TaskContext context)
        {
            this.db = context;
        }
        public RepositoryActionResult<TaskLabel> Create(TaskLabel label)
        {
            try
            {
                db.TaskLabels.Add(label);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<TaskLabel>(label, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<TaskLabel>(label, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<TaskLabel>(null, RepositoryActionStatus.Error, e);
            }
        }
    

        public RepositoryActionResult<TaskLabel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskLabel> Find(Func<TaskLabel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TaskLabel Get(int Id)
        {
            return db.TaskLabels.FirstOrDefault(lbl => lbl.Id == Id);
        }

        public IQueryable<TaskLabel> GetAll()
        {
            return db.TaskLabels;
        }

        public IQueryable<TaskLabel> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<TaskLabel> Update(TaskLabel item)
        {
            throw new NotImplementedException();
        }
    }
}
