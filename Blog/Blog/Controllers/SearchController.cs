using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;

namespace Blog.Controllers
{
    public class SearchController : Controller
    {
        private PostService postService = new PostService();
        
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public void SearchByTag(long id)
        {
           /* try
            {
                var pageViewModel = new PageViewModel<PostViewModel>
                {
                    Data = postService.select(post => post= new PageViewModel<>
                }
            }*/
        }

        public void SearchByText(String text)
        {
            
        }
    }
}