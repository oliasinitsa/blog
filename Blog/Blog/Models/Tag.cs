using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Blog.Services;

namespace Blog.Models
{
    public class Tag : BaseEntity
    {
        [MinLength(3)]
        public String Text { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }

        public Tag(String text, Post post)
        {
            this.Posts = new List<Post>();
            this.Text = text;
            this.Posts.Add(post);
        }

    }
}