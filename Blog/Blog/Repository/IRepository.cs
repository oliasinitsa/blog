using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);

        IQueryable<TEntity> Read();

        TEntity ReadById(long id);

        void Update(TEntity entity);

        void Delete(long id);
    }
}
