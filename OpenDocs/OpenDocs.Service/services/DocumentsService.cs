using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Linq;

namespace OpenDocs.Service
{
    [DefaultView("Documents")]
    public class DocumentsService : ServiceStack.Service
    {
        public object Get(DocumentsSearch request)
        {
            return new Documents
            {
                Total = Db.Scalar<int>("select count(*) from Document"),
                Results = request.Id != default(int)
                    ? Db.Select<Document>(d => d.Id == request.Id)
                    : request.Query != null
                        ? Db.Select<Document>(d => d.Title.Contains(request.Query) || d.Content.Contains(request.Query))
                            .OrderBy(d => d.Updated)
                            .ToList()
                        : Db.Select<Document>()
            };
        }

        public object Get(Document request)
        {
            var document = Db.Select<Document>(q => q.Id == request.Id)[0];

            return new HttpResult(document)
            {
                View = Convert.ToString(document.DocumentType)
            };
        }

        public object Post(Document local)
        {
            var server = Db.Select<Document>(q => q.Id == local.Id)[0];
            server.Title = local.Title;

            // Client has a newer version
            if (server.Revision < local.Revision)
            {
                var text1 = server.Content ?? "";
                var text2 = local.Content ?? "";

                var dmp = new DiffMatchPatch.diff_match_patch();

                // Compute patch required between the old and new text
                var patches = dmp.patch_make(text1, text2);

                // Apply patch to old text
                var result = Convert.ToString(dmp.patch_apply(patches, text1)[0]);

                server.Content = result;
                server.Revision = local.Revision;
                server.Updated = DateTime.Now;
            }

            Db.Save(server);

            // Send latest document to client
            return server;
        }

        public object Get(DocumentCreate request)
        {
            return new HttpResult(request)
            {
                View = "Create"
            };
        }

        public object Post(DocumentCreate request)
        {
            var document = new Document()
            {
                Title = "New " + Convert.ToString(request.DocumentType),
                DocumentType = request.DocumentType,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            Db.Save(document);

            return new HttpResult(document)
            {
                Location = "/documents/" + document.Id
            };
        }

        public object Post(DocumentDelete request)
        {
            Db.DeleteById<Document>(request.Id);

            return Get(new DocumentsSearch());
        }
    }
}
