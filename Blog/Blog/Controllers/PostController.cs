using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Blog.Models;
using Blog.Services;
using Blog.ViewModel;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService postService = new PostService();
        private readonly CommentService commentService = new CommentService();
        private readonly TagService tagService = new TagService();

        // GET: Post1
        public ActionResult PostView(long id = 0, int page = 1)
        {
            try
            {
                var postViewModel = new object();
                if (id == 0)
                {
                    postViewModel = new PostViewModel
                    {
                        Post = (Post) postService.Read().OrderByDescending(post => post.Changed).Take(1),
                        pageViewModel = new PageViewModel<Comment>()
                        {
                            Data =
                                postService.ReadById(id)
                                    .Comments.OrderByDescending(comment => comment.Created)
                                    .Skip(page == 1 ? 0 : (6 + page*6))
                                    .Take(6)
                                    .ToList(),
                            PageNumber =
                                postService.ReadById(id).Comments.Count%6 == 0
                                    ? (postService.ReadById(id).Comments.Count/6)
                                    : (postService.ReadById(id).Comments.Count/6) + 1,
                            CurrentPage = page

                        }
                    };

                }
                else
                {
                    postViewModel = new PostViewModel
                    {
                        Post = postService.ReadById(id),
                        pageViewModel = new PageViewModel<Comment>()
                        {
                            Data =
                                postService.ReadById(id)
                                    .Comments.OrderByDescending(comment => comment.Created)
                                    .Skip(page == 1 ? 0 : (6 + page*6))
                                    .Take(6)
                                    .ToList(),
                            PageNumber =
                                postService.ReadById(id).Comments.Count%6 == 0
                                    ? (postService.ReadById(id).Comments.Count/6)
                                    : (postService.ReadById(id).Comments.Count/6) + 1,
                            CurrentPage = page

                        }
                    
                    };

                }
                return View(postViewModel);
            }
            catch (Exception exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult PostViewComment(long id ,int page)
        {
            try
            {
                var pageViewModel = new PageViewModel<Comment>()
                {
                    Data =
                        postService.ReadById(id)
                            .Comments.OrderByDescending(comment => comment.Created)
                            .Skip(6 + page*6)
                            .Take(6)
                            .ToList(),
                    PageNumber =
                        postService.ReadById(id).Comments.Count%6 == 0
                            ? (postService.ReadById(id).Comments.Count/6)
                            : (postService.ReadById(id).Comments.Count/6) + 1,
                    CurrentPage = page

                };
                return View("_CommentPagePartial", pageViewModel);
            }
            catch (Exception exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Posts(int page = 1)
        {
            try
            {
                if (page > 0)
                {
                    var pageViewModel = new PageViewModel<PostViewModel>
                    {
                        Data =
                            postService.Read().Select(post => new PostViewModel { Post = post})
                                .OrderByDescending(post => post.VotesValue)
                                .Skip(page == 1 ? 0 : (6 * (page - 1)))
                                .Take(6)
                                .ToList(),
                        PageNumber =
                            postService.Read().Count()%6 == 0
                                ? postService.Read().Count()
                                : postService.Read().Count() + 1,
                        CurrentPage = page
                    };
                    return View(pageViewModel);
                }
                throw new Exception();
            }
            catch (Exception excpetion)
            {
                return HttpNotFound();
            }  
        }

        public ActionResult PostPages(int page)
        {
            try
            {
                var pageViewModel = new PageViewModel<PostViewModel>
                {
                    Data =
                        postService.Read().Select(post => new PostViewModel { Post = post })
                            .OrderByDescending(post => post.VotesValue)
                            .Skip(6 * (page - 1))
                            .Take(6)
                            .ToList(),
                    PageNumber =
                        postService.Read().Count() % 6 == 0
                            ? postService.Read().Count()
                            : postService.Read().Count() + 1,
                    CurrentPage = page
                };
                return View("_PostsPartial", pageViewModel);
            }
            catch (Exception exception)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public void AddComment(long id, String text)
        {
            
                if(Request.IsAuthenticated)
                    commentService.Create(new Comment(text, id, User.Identity.GetUserId()));
        }

        [HttpGet]
        public void DeleteComment(long id)
        {
            if(commentService.ReadById(id).UserId == User.Identity.GetUserId())
                commentService.Delete(id);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangePost(long id)
        {
            return View("Add", postService.ReadById(id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeletePost(long id)
        {
            postService.Delete(id);

            return View("Posts");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View(new Post());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Post post)
        {
            if (ModelState.IsValid)
            {
                if (post.Id != null)
                {
                    postService.Update(post);
                }
                else
                {
                    postService.Create(post);
                }
                
                var tags = post.tag.Split(';').AsEnumerable();
                foreach (var tag in tags)
                {
                    if (tagService.GetAllTags().All(tg => tg.Text != tag))
                    {
                        tagService.Create(new Tag(tag, post));
                    }
                    else
                    {
                        tagService.GetAllTags().First(tg => tg.Text == tag).Posts.Add(post);
                    }
                }
              //  foreach (var tag in tags)
               // {
                 /*  */
                    
               // }
                return RedirectToAction("PostView", new { id = post.Id, page = 1 });
            }

            return View(post);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                postService.Delete(id);
                return RedirectToAction("Posts");
            }

            return HttpNotFound();
        }


    }
}