﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}