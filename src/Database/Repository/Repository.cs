using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context _context;

        protected Repository(Context context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(Get(id));
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
            _context.SaveChanges();
        }

        public void Update(int id, object item)
        {
            _context.Entry(Get(id)).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }
    }
}
