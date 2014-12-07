using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public String Text { get; set; }

        public DateTime Created { get; set; }

        public long PostId { get; set; }

        public String UserId { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
            
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Comment()
        {
            Votes = new List<Vote>();
        }

        public Comment(String text, long postId, String userId)
        {
            Votes = new List<Vote>();
            this.Text = text;
            this.PostId = postId;
            this.UserId = userId;
            this.Created = DateTime.Now;
        }
    }
}