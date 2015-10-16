using Funq;
using OpenDocs.Service;
using OpenDocs.Service.Model;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Razor;
using System;

namespace OpenDocs
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("OpenDocs", typeof(DocumentsService).Assembly) { }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ConsoleLogFactory();

            Plugins.Add(new RazorFormat());

            SetConfig(new HostConfig { DebugMode = true });

            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(@"Data Source=.\SQLEXPRESS;Initial Catalog=OpenDocs;Integrated Security=SSPI", SqlServerDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().OpenDbConnection())
            {
                db.CreateTableIfNotExists<Document>();
            }
        }
    }
}