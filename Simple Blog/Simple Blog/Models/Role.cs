using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Blog.Models
{
    public class Role
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Role - ID: {0}, Name: {1}", ID, Name);
        }
    }

    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(x => x.ID, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}
