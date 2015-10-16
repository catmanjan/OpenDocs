using OpenDocs.Service.Model;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;

namespace OpenDocs.Service
{
    [Route("/documents/{Id}")]
    [Route("/documents", "POST")]
    [DefaultView("Document")]
    public class Document
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int Revision { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Document() { }
    }
}
