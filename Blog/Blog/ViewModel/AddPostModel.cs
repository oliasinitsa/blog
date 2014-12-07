using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModel
{
    public class AddPostModel
    {
        public Post Post { get; set; }

        public String Title
        {
            get { return Post.Title; }
            set { Post.Title = Title; }
        }

        public String Text
        {
            get { return Post.Text; }
            set { Post.Text = Text; }
        }

        public String Tags
        {
            get
            {
                return Post.Tags.Aggregate("", (current, tag) => current + (tag.Text + "; "));
            }
            
        }
    }
}