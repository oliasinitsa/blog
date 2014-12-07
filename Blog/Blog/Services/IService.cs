using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services
{
    public interface IService<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);

        IEnumerable<TEntity> Read();

        TEntity ReadById(long id);

        void Update(TEntity entity);

        void Delete(long id);
    }
}
