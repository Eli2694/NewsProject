using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL
{
    public interface IRepository<TEntity> where TEntity : class // TEntity can only be reference type , not primitive type
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(int id);
        Task DeleteAsync(int id);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // prevent accidental reassignment of the property in the repository class after it has been initialized
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        //async
        
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }


        //async
         
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        
        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        //async
        
        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Task.Run(() => {
                _dbSet.Add(entity);
                _context.SaveChanges();
            });
        }
        

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        // async 
        
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // the entity parameter is already updated with new information. 
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
       
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // the entity parameter ia already updated with new information. 
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //async
        
        public async Task DeleteAsync(int id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
        
        public void Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }
    }

    // example of implamintation of this functions from the layer above:
    /*
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity);
    }
    */

}
