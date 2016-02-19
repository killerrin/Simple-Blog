using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using Simple_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Simple_Blog
{
    public static class Database
    {
        private const string SessionKey = "SimpleBlog.Database.SessionKey";
        private static ISessionFactory _sessionFactory;

        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();

            // Configure the Connection String
            config.Configure();

            // Add our Mappings
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<TagMap>();
            mapper.AddMapping<PostMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            // Create our session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}
