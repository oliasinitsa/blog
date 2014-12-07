using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services
{
    public class PostService : BaseService<Post>, IService<Post>
    {
        private readonly IRepository<Post> posts;

        public PostService()
        {
            posts = new BaseRepository<Post>(context);
        }

        public void Create(Post entity)
        {
            entity.Created = DateTime.Now;
            entity.Changed = entity.Created;
            posts.Create(entity);
            Save();
        }

        public IEnumerable<Post> Read()
        {
            return posts.Read().AsEnumerable();
        }

        public Post ReadById(long id)
        {
            return posts.ReadById(id);
        }

        public void Update(Post entity)
        {
            posts.Update(entity);
            Save();
        }

        public void Delete(long id)
        {
            posts.Delete(id);
            Save();
        }

        public IEnumerable<Post> GetPagePosts(int numberOfPage)
        {
            return posts.Read().OrderByDescending(post => post.Changed).Skip((numberOfPage - 1)*10).Take(10).AsEnumerable();
        }

        public IEnumerable<Post> GetThreeLastPosts()
        {
            return posts.Read().OrderByDescending(post => post.Changed).Take(3).AsEnumerable();
        }

    }
}