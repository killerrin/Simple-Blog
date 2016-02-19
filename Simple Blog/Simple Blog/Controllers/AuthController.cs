using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Simple_Blog.ViewModels;
using NHibernate.Linq;
using Simple_Blog.Models;

namespace Simple_Blog.Controllers
{
	public class AuthController : Controller
	{
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToRoute("home");
		}
		
		public ActionResult Login()
		{
			return View(new AuthLogin());
		}
		
		[HttpPost]
		public ActionResult Login(AuthLogin form, string returnUrl)
		{
			var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);

			// Prevent Timing Attacks
			if (user == null)
				Simple_Blog.Models.User.FakeHash();

			// Check Password and add Model error if incorrect
			if (user == null || !user.CheckPassword(form.Password))
				ModelState.AddModelError("Username", "Username or Password is incorrect");

			if (!ModelState.IsValid)
				return View(form);

			FormsAuthentication.SetAuthCookie(user.Username, true);
			
			if (!string.IsNullOrWhiteSpace(returnUrl))
				return Redirect(returnUrl);

			return RedirectToRoute("home");
		}
	}
}