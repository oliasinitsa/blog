using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;
using Microsoft.Ajax.Utilities;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostService postService = new PostService();
        private readonly CommentService commentService = new CommentService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayTagsCloud()
        {
            var tags = new TagService().GetAllTags();
          //  if (tags.GetAllTags().Count() == 0)
            //    return Content(GlobalRes.NoTags);
            return PartialView("_TagsCloudPartial", tags);
        }

        public ActionResult ThreeLastPosts()
        {
            var posts = postService.Read().Select(post => new PostViewModel {Post = post})
                .OrderByDescending(post => post.Post.Created)
                .Take(3)
                .ToList();
            return PartialView("_ThreeLastPostsPartial", posts);
        }
        public ActionResult ThreeLastComments()
        {
            var comments = commentService.Read().Select(comment => new CommentViewModel { Comment = comment })
                .OrderByDescending(comment => comment.Comment.Created)
                .Take(3)
                .ToList();
            return PartialView("_ThreeLastCommentsPartial", comments);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}