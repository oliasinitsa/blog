using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post : BaseEntity
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public String Title { get; set; }

        [MinLength(150)]
        public String Text { get; set; }

        public DateTime Created { get; set; }

        public DateTime Changed { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public String tag
        {
            get
            {
                return Tags.Aggregate("", (current, tag) => current + (tag.Text + "; "));
            }
            set
            {
                var str = value.Split(';').AsEnumerable();
                foreach (var oneTag in str)
                {
                    
                }
            }

        }

        public Post()
        {
            Votes = new List<Vote>();
            Tags = new List<Tag>();
            Comments = new List<Comment>();
        }


    }
}