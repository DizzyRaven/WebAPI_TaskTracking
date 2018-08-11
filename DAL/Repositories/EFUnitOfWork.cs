using System;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TaskContext db;
        private UserContext userdb;
        private TaskRepository taskRepository;
        private PerformerRepository performerRepository;
        private SubTaskRepository subTaskRepository;
        private TaskLabelRepository taskLabelRepository;

        public EFUnitOfWork(String connectionString)
        {
            db = new TaskContext(connectionString);
            userdb = new UserContext(connectionString);
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                    taskRepository = new TaskRepository(db);
                return taskRepository;
            }
        }
        public IRepository<TaskLabel> Labels
        {
            get
            {
                if (taskLabelRepository == null)
                    taskLabelRepository = new TaskLabelRepository(db);
                return taskLabelRepository;
            }
        }
        public IRepository<Performer> Performers
        {
            get
            {
                if (performerRepository == null)
                    performerRepository = new PerformerRepository(userdb);
                return performerRepository;
            }
        }
        public IRepository<SubTask> SubTasks
        {
            get
            {
                if (subTaskRepository == null)
                    subTaskRepository = new SubTaskRepository(db);
                return subTaskRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        // TODO Dispose pattern
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
