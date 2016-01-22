using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Simple_Blog.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
