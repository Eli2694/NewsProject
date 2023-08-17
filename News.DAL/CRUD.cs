using Microsoft.EntityFrameworkCore;

namespace News.DAL
{
    public interface IRepository<TEntity> where TEntity : class // TEntity can only be reference type , not primitive type
    {
        IEnumerable<TEntity> GetAll();
       
        TEntity GetById(int id);
      
        void Insert(TEntity entity);
       
        void Update(TEntity entity);
       
        void Delete(int id);
      
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // prevent accidental reassignment of the property in the repository class after it has been initialized
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private static object _lockObject = new object();

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        
        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                // Critical Section
                lock (_lockObject)
                {
                    return _dbSet.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
     
        }
  
        public TEntity GetById(int id)
        {
            try
            {
                // Critical Section
                lock (_lockObject)
                {
                    return _dbSet.Find(id);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                // Critical Section
                lock (_lockObject)
                {
                    _dbSet.Add(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }     
        }
       
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                // Critical Section
                lock (_lockObject)
                {
                    // the entity parameter ia already updated with new information. 
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        
        public void Delete(int id)
        {
            try
            {
                lock (_lockObject)
                {
                    TEntity entityToDelete = _dbSet.Find(id);
                    if(entityToDelete != null) 
                    {
                        _dbSet.Remove(entityToDelete);
                        _context.SaveChanges();
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }

           
        }
    }

}
