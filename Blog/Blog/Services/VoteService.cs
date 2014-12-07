using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services
{
    public class VoteService : BaseService<Vote>
    {
        private readonly IRepository<Vote> votes;

        public VoteService()
        {
            votes = new BaseRepository<Vote>(context);
        }

        public void Create(Vote entity)
        {
            votes.Create(entity);
            Save();
        }

        public void Delete(long id)
        {
            votes.Delete(id);
            Save();
        }

        public void Update(Vote vote)
        {
            votes.Update(vote);
            Save();
        }

        public void VoteUp(long id)
        {
            var vote = votes.ReadById(id);
            vote.Value++;
            votes.Update(vote);
            Save();
        }

        public void VoteDown(long id)
        {
            var vote = votes.ReadById(id);
            vote.Value--;
            votes.Update(vote);
            Save();
        }

        public Vote getCommentVote(long id)
        {
            return votes.Read().Single(vote => vote.Comment.Id == id);
        }

        public Vote getPostVote(long id)
        {
            return votes.Read().Single(vote => vote.Post.Id == id);
        }
    }
}