using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Models;
using Blog.Services;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {

        private CommentService commentService = new CommentService();
        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            try
            {
                commentService.Create(comment);
                return RedirectToAction("PostView", new {id = comment.PostId});
            }
            catch (Exception exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult UpdateComment(Comment comment)
        {
            try
            {
                commentService.Update(comment);
                return RedirectToAction("PostView", new {id = comment.PostId});
            }
            catch (Exception exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult DeleteComment(Comment comment)
        {
            try
            {
                commentService.Delete(comment.Id);
                return RedirectToAction("PostView", new { id = comment.PostId });
            }
            catch (Exception exception)
            {
                return HttpNotFound();
            }
        }
    }
}