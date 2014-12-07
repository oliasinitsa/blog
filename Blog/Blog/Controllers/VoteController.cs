using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.Repository;
using Blog.Services;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class VoteController : Controller
    {
        private VoteService voteService = new VoteService();
        private CommentService commentService = new CommentService();
        private PostService postService = new PostService();
        private bool CheckVotePost(Post post)
        {
            return post.Votes.All(vote => vote.UserId != User.Identity.GetUserId());
        }

        private bool CheckVoteComment(Comment comment)
        {
            return comment.Votes.All(vote => vote.UserId != User.Identity.GetUserId());
        }
        
        public ActionResult VoteComment(long id)
        {
            var comment = commentService.ReadById(id);
            if (Request.IsAuthenticated)
            {
                AddVoteComment(comment);
            }
            return Json(CountVotesComment(comment), JsonRequestBehavior.AllowGet);
        }

        private void AddVoteComment(Comment comment)
        {
            if (CheckVoteComment(comment))
            {
                comment.Votes.Add(new Vote(comment, User.Identity.GetUserId(), 1));
                commentService.Update(comment);
            }
            else
            {
                var uservote = comment.Votes.First(vote => vote.UserId == User.Identity.GetUserId());
                if (uservote.Value == 1)
                {
                    uservote.Value = 0;
                    commentService.Update(comment);
                }
                else
                {
                    uservote.Value = 1;
                    commentService.Update(comment);
                }
            }
        }

        public ActionResult VotePost(long id)
        {
            var post = postService.ReadById(id);
            if (Request.IsAuthenticated)
            {
                AddVotePost(post);
            }
            return Json(CountVotesPost(post), JsonRequestBehavior.AllowGet);
        }

        public void AddVotePost(Post post)
        {
            if (CheckVotePost(post))
            {
                post.Votes.Add(new Vote(post, User.Identity.GetUserId(), 1));
                postService.Update(post);
            }
            else
            {
                var uservote = post.Votes.First(vote => vote.UserId == User.Identity.GetUserId());
                if (uservote.Value == 1)
                {
                    uservote.Value = 0;
                    postService.Update(post);
                }
                else
                {
                    uservote.Value = 1;
                    postService.Update(post);
                }
                
            }
        }

        public int CountVotesPost(Post post)
        {
            return post.Votes.Sum(a => a.Value);
        }

        public int CountVotesComment(Comment comment)
        {
            return comment.Votes.Sum(a => a.Value);
        }
    }
}