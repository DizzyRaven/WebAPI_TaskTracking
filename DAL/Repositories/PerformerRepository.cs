using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;

namespace DAL.Repositories
{
    class PerformerRepository : IRepository<Performer>
    {
        private UserContext db;

        public PerformerRepository(UserContext context)
        {
            this.db = context;
        }
        public RepositoryActionResult<Performer> Create(Performer item)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<Performer> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Performer> Find(Func<Performer, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Performer Get(int Id)
        {
            return db.Performers.FirstOrDefault(x => x.Id == Id);
        }

        public IQueryable<Performer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Performer> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public RepositoryActionResult<Performer> Update(Performer item)
        {
            throw new NotImplementedException();
        }
    }
}
