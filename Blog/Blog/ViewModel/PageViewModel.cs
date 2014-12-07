using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModel
{
    public class PageViewModel<TModel> where TModel : class
    {
        public List<TModel> Data { get; set; }

        public int PageNumber { get; set; }

        public int CurrentPage { get; set; }
    }
}