using News.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            catch (DbUpdateException ex)
            {
                // Handle database connection error
                var innerException = ex.InnerException;
                var sqlException = innerException as SqlException;

                if (sqlException != null && (sqlException.Number == 4060 || sqlException.Number == 18456))
                {
                    // Connection error occurred
                    throw new Exception("Cannot connect to database. Please check the connection string and try again.");
                }
                else
                {
                    // Other database error occurred
                    throw new Exception("Error occurred while trying to insert record into database.");
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
            catch (DbUpdateException ex)
            {
                // Handle database connection error
                var innerException = ex.InnerException;
                var sqlException = innerException as SqlException;

                if (sqlException != null && (sqlException.Number == 4060 || sqlException.Number == 18456))
                {
                    // Connection error occurred
                    throw new Exception("Cannot connect to database. Please check the connection string and try again.");
                }
                else
                {
                    // Other database error occurred
                    throw new Exception("Error occurred while trying to insert record into database.");
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
            catch (DbUpdateConcurrencyException ex) // Optimistic Concurrency
            {
                // Handle concurrency conflict
                /*
                var entry = ex.Entries.Single();
                This line gets the DbEntityEntry instance that caused the concurrency conflict. 
                The Single() method is used to ensure that there is only one entry, as there can be multiple entries in a single exception if multiple entities were affected. */
                var entry = ex.Entries.Single();

                /*
                 var clientValues = (TEntity)entry.Entity;
                 var databaseValues = (TEntity)entry.GetDatabaseValues().ToObject(); 
                 These lines get the client-side and database-side values of the entity that caused the conflict. The TEntity type parameter is used to cast the values to the appropriate type.
                 */
                var clientValues = (TEntity)entry.Entity;
                var databaseValues = (TEntity)entry.GetDatabaseValues().ToObject();

                // Determine which values are different
                /*
                 *This line determines which properties of the entity have been modified on the client side by comparing the current and original values of each property. The PropertyNames property is used to get the names of all the properties of the entity. 
                 */
                var modifiedProperties = entry.CurrentValues.PropertyNames
                    .Where(name => !Equals(entry.CurrentValues[name], entry.OriginalValues[name]))
                    .ToList();

                // Update the values that have not been modified in the database
                /*
                 * This line updates the original values of the entity properties that have not been modified on the client side with the corresponding database-side values.
                 */
                modifiedProperties.ForEach(property => entry.Property(property).OriginalValue = databaseValues.GetType().GetProperty(property).GetValue(databaseValues));

                // Try again to save changes
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex2)
                {
                    // Handle concurrency conflict again or throw exception
                    throw new Exception("Concurrency conflict occurred while updating record.");
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle database connection error
                var innerException = ex.InnerException;
                var sqlException = innerException as SqlException;

                if (sqlException != null && (sqlException.Number == 4060 || sqlException.Number == 18456))
                {
                    // Connection error occurred
                    throw new Exception("Cannot connect to database. Please check the connection string and try again.");
                }
                else
                {
                    // Other database error occurred
                    throw new Exception("Error occurred while trying to insert record into database.");
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
            catch (DbUpdateException ex)
            {
                // Handle database connection error
                var innerException = ex.InnerException;
                var sqlException = innerException as SqlException;

                if (sqlException != null && (sqlException.Number == 4060 || sqlException.Number == 18456))
                {
                    // Connection error occurred
                    throw new Exception("Cannot connect to database. Please check the connection string and try again.");
                }
                else
                {
                    // Other database error occurred
                    throw new Exception("Error occurred while trying to insert record into database.");
                }
            }
            catch (Exception)
            {

                throw;
            }

           
        }
    }

}
