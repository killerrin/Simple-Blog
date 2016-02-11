﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Simple_Blog.Controllers;

namespace Simple_Blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var namespaces = new[] { typeof(PostsController).Namespace };
            routes.MapRoute("Login", "login", new { controller = "Auth", Action = "Login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", Action = "Logout" }, namespaces);

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, namespaces);
        }
    }
}
