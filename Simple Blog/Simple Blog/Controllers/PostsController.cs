using NHibernate.Linq;
using Simple_Blog.Infrastructure;
using Simple_Blog.Models;
using Simple_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Simple_Blog.Controllers
{
    public class PostsController : Controller
    {
        private const int PostsPerPage = 10;

        public ActionResult Index(int page = 1)
        {
            var baseQuery = Database.Session.Query<Post>()
                .Where(t => t.DeletedAt == null)
                .OrderByDescending(t => t.CreatedAt);

            var totalPostCount = baseQuery.Count();
            var postIDs = baseQuery
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Select(t => t.ID)
                .ToArray();
            var posts = baseQuery
                .Where(t => postIDs.Contains(t.ID))
                .FetchMany(t => t.Tags)
                .Fetch(t => t.User)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(posts, totalPostCount, page, PostsPerPage)
            });
        }

        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            var parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            var tag = Database.Session.Load<Tag>(parts.Item1);
            if (tag == null)
                return HttpNotFound();

            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Tag", new { id = parts.Item1, slug = tag.Slug });

            var totalPostCount = tag.Posts.Count();
            var postIDs = tag.Posts
                .OrderByDescending(g => g.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Where(t => t.DeletedAt == null)
                .Select(t => t.ID)
                .ToArray();
            var posts = Database.Session.Query<Post>()
                .OrderByDescending(b => b.CreatedAt)
                .Where(t => postIDs.Contains(t.ID))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.User)
                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PagedData<Post>(posts, totalPostCount, page, PostsPerPage)
            });
        }

        public ActionResult Show(string idAndSlug)
        {
            var parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();

            var post = Database.Session.Load<Post>(parts.Item1);
            if (post == null || post.IsDeleted)
                return HttpNotFound();

            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });

            return View(new PostsShow
            {
                Post = post
            });
        }

        private System.Tuple<int, string> SeperateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");

            return Tuple.Create(id, slug);
        }

    }
}
