using Simple_Blog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple_Blog.Areas.Admin.Controllers
{
	[Authorize(Roles = "admin")]
	[SelectedTabAttribute("users")]
	public class UsersController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}