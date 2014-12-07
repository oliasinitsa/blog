using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services
{
    public class BaseService<TEntity>
    {
        protected readonly ApplicationDbContext context = new ApplicationDbContext();


        public void Save()
        {
            context.SaveChanges();
        }


    }
}