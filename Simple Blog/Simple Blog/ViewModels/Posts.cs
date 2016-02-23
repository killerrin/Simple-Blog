using Simple_Blog.Infrastructure;
using Simple_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Blog.ViewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }

    public class PostsShow
    {
        public Post Post { get; set; }
    }

    public class PostsTag
    {
        public Tag Tag { get; set; }
        public PagedData<Post> Posts { get; set; }
    }
}
