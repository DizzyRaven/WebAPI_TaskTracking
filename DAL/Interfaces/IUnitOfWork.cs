using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IRepository<Task> Tasks { get; }
        IRepository<SubTask> SubTasks { get; }
        IRepository<Performer> Performers { get;  }
        void Save();
    }
}
