using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using News.Model;

namespace News.DAL
{
    // DbContext manages the connection to the database automatically and will open and close the connection as needed.
    public partial class DataLayer : DbContext
    {

        // CRUD Operation
        private readonly Repository<Users> _usersRepository;
        private readonly Repository<Article> _articleRepository;
        private readonly Repository<Category> _categoryRepository;
        private readonly Repository<UserClick> _userClickRepository;

        public IRepository<Users> UsersRepository => _usersRepository;
        public IRepository<Article> ArticleRepository => _articleRepository; // => represent get {return _articleRepository }
        public IRepository<Category> CategoryRepository => _categoryRepository;
        public IRepository<UserClick> UserClickRepository => _userClickRepository;

        private static string connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=NewsProject; Integrated Security=SSPI;Persist Security Info=False";

        private DataLayer() : base(connectionString)
        {

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataLayer>());

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

        private void Seed()
        {
            // Seed categories
            var categories = GetRSSFeeds();
            Categories.AddRange(categories);

            // Seed configuration
            Model.Configuration SeedConfiguration = new News.Model.Configuration()
            {
                Auth0Bearer = "",              
            };

            Configuration.Add(SeedConfiguration);

            //שמירת שינויים במסד נתונים
            SaveChanges();
        }

        // Configure database model
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserClick>()
        //        .HasRequired(u => u.Category)
        //        .WithMany()
        //        .HasForeignKey(u => u.CategoryID)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Article>()
        //        .HasRequired(u => u.Category)
        //        .WithMany()
        //        .HasForeignKey(u => u.CategoryID)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<UserClick>()
        //        .HasRequired(u => u.Article)
        //        .WithMany()
        //        .HasForeignKey(u => u.ArticleID)
        //        .WillCascadeOnDelete(false);

        //}



        //DbSet- פקודה ליצירת טבלאות בדטה בייס
        public DbSet<Users> Users { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<UserClick> UserClicks { get; set; }

        public DbSet<Model.Configuration> Configuration { get; set; }

        
    }
}
