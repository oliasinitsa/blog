using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModel
{
    public class PostViewModel : CommentViewModel
    {
        public Post Post { get; set; }

        public PageViewModel<Comment> pageViewModel { get; set; }

        public DateTime Publishing
        {
            get
            {
                return Post.Changed;
            }
        }
     

        public int VotesValue
        {
            get { return Post.Votes.Sum(vote => vote.Value); }
        }

        public IEnumerable<String> TagsPost
        {
            get { return Post.Tags.Select(tag => tag.Text).AsEnumerable(); }
        }

        public IEnumerable<Comment> Comments
        {
            get { return Post.Comments.OrderByDescending(comment => comment.Created).AsEnumerable(); }
        }

        public String PostName
        {
            get { return Post.Title; }
        }

        public String PostText
        {
            get { return Post.Text; }
        }
    }
}