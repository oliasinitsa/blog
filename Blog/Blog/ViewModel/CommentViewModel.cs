using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModel
{
    public class CommentViewModel
    {
        public Comment Comment { get; set; }

        public int VotesValue
        {
            get { return Comment.Votes.Sum(vote => vote.Value); }
        }

        public String UserName
        {
            get { return Comment.User.UserName; }
        }

        public String Text
        {
            get { return Comment.Text; }
        }
    }
}