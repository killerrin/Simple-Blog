using Simple_Blog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Blog.Areas.Admin.ViewModels
{
    public class RoleCheckbox
    {
        public int ID { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return String.Format("RoleCheckbox - ID: {0}, IsChecked: {1}, Name: {2}", ID, IsChecked, Name);
        }
    }

    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UsersNew
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class UsersEdit
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class UsersResetPassword
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
