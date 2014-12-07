using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Repository;
using Microsoft.Ajax.Utilities;

namespace Blog.Services
{
    public class CommentService : BaseService<Comment>, IService<Comment>
    {
        private IRepository<Comment> comments;

        public CommentService()
        {
            comments = new BaseRepository<Comment>(context);
        }

        public void Create(Comment entity)
        {
            entity.Created = DateTime.Now;
            comments.Create(entity);
            Save();
        }

        public IEnumerable<Comment> Read()
        {
            return comments.Read().AsEnumerable();
        }

        public Comment ReadById(long id)
        {
            return comments.ReadById(id);
        }

        public void Update(Comment entity)
        {
            comments.Update(entity);
            Save();
        }

        public void Delete(long id)
        {
            comments.Delete(id);
            Save();
        }

        public IEnumerable<Comment> GetThreeLastComments()
        {
            return comments.Read().OrderByDescending(comment => comment.Created).Take(3).AsEnumerable();
        }

        public IEnumerable<Comment> GetCommentsByPost(long postId)
        {
            return comments.Read().Where(comment => comment.PostId.Equals(postId)).AsEnumerable();
        }
    }
}