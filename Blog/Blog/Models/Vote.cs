using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Vote : BaseEntity
    {
        public String UserId { get; set; }

        public int Value { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        
        public virtual Post Post { get; set; }

        
        public virtual Comment Comment { get; set; }

        public Vote()
        {
            
        }

        public Vote(Comment comment, String userId, int value)
        {
            this.Comment = comment;
            this.UserId = userId;
            this.Value = value;
        }

        public Vote(Post post, String userId, int value)
        {
            this.Post = post;
            this.UserId = userId;
            this.Value = value;
        }
    }
}