using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using News.DAL;
using System.Xml.Linq;
using News.Model;

namespace News.DAL
{
    // DbContext manages the connection to the database automatically and will open and close the connection as needed.
    public partial class DataLayer : DbContext
    {
        //private static readonly Lazy<DataLayer> _lazyData = new Lazy<DataLayer>(() => new DataLayer());

        // CRUD Operation
        private readonly Repository<Users> _usersRepository;
        private readonly Repository<Article> _articleRepository;
        private readonly Repository<Category> _categoryRepository;
        private readonly Repository<UserClick> _userClickRepository;

        public IRepository<Users> UsersRepository => _usersRepository;
        public IRepository<Article> ArticleRepository => _articleRepository; // => represent get {return _articleRepository }
        public IRepository<Category> CategoryRepository => _categoryRepository;
        public IRepository<UserClick> UserClickRepository => _userClickRepository;

        private static string connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=News_Project; Integrated Security=SSPI;Persist Security Info=False";

        private DataLayer() : base(connectionString)
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<DataLayer>());

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
        //public static DataLayer Data => _lazyData.Value;

        // Singleton instance
        private static DataLayer _data;
        public static DataLayer Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new DataLayer();
                }
                return _data;
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


