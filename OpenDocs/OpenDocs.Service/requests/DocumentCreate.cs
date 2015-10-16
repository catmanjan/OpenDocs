using OpenDocs.Service.Model;
using ServiceStack;

namespace OpenDocs.Service
{
    [Route("/documents/create")]
    public class DocumentCreate
    {
        public DocumentType DocumentType { get; set; }
    }
}
