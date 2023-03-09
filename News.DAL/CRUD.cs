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
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception)
            {

                throw;
            }

            
        }


        //async
         
        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
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
                return _dbSet.Find(id);
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        //async
        
        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await Task.Run(() => {
                    _dbSet.Add(entity);
                    _context.SaveChanges();
                });
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
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        // async 
        
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                // the entity parameter is already updated with new information. 
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
                // the entity parameter ia already updated with new information. 
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        //async
        
        public async Task DeleteAsync(int id)
        {
            try
            {
                TEntity entityToDelete = await _dbSet.FindAsync(id);
                _dbSet.Remove(entityToDelete);
                await _context.SaveChangesAsync();
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
                TEntity entityToDelete = _dbSet.Find(id);
                _dbSet.Remove(entityToDelete);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

           
        }
    }

}
