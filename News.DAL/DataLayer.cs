
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using News.Model;

namespace News.DAL
{
    // DbContext manages the connection to the database automatically and will open and close the connection as needed.
    public partial class DataLayer : DbContext, IDataLayer
    {
        //private static readonly Lazy<DataLayer> _lazyData = new Lazy<DataLayer>(() => new DataLayer());

        // CRUD Operation
        private readonly Repository<Users> _usersRepository;
        private readonly Repository<Article> _articleRepository;
        private readonly Repository<Category> _categoryRepository;
        private readonly Repository<UserClick> _userClickRepository;

        public IRepository<Users> UsersRepository => _usersRepository;
        public IRepository<Article> ArticleRepository => _articleRepository;
        public IRepository<Category> CategoryRepository => _categoryRepository;
        public IRepository<UserClick> UserClickRepository => _userClickRepository;



        public DataLayer(DbContextOptions<DataLayer> options) : base(options)
        {

            try
            {
                ChangeTracker.LazyLoadingEnabled = false;



                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }


            }
            catch (Exception)
            {
   
                throw;
            }

            _usersRepository = new Repository<Users>(this);
            _articleRepository = new Repository<Article>(this);
            _categoryRepository = new Repository<Category>(this);
            _userClickRepository = new Repository<UserClick>(this);

            try
            {
                if (!Categories.Any())
                {
                    Seed();
                }
            }
            catch (ArgumentNullException)
            {

                throw;
            }        

        }

        private static object seedLock = new object(); // Shared lock object

        private void Seed()
        {
            lock (seedLock) // Acquire the lock
            {
                if (!Categories.Any())
                {
                    var categories = GetRSSFeeds();

                    foreach (var category in categories)
                    {
                        if (!Categories.Any(c => c.name == category.name && c.source == category.source))
                        {
                            Categories.Add(category);
                        }
                    }

                    SaveChanges();
                }
            } 
        }


        //DbSet- פקודה ליצירת טבלאות בדטה בייס
        public DbSet<Users> Users { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<UserClick> UserClicks { get; set; }

        public DbSet<Model.Configuration> Configuration { get; set; }
        
    }
}


