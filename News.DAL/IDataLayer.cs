using News.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL
{
    public interface IDataLayer
    {
        IRepository<Users> UsersRepository { get; }
        IRepository<Article> ArticleRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<UserClick> UserClickRepository { get; }

    }

}
