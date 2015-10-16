using ServiceStack;

namespace OpenDocs.Service
{
    [Route("/documents")]
    [Route("/documents/search/{Query}")]
    public class DocumentsSearch
    {
        public string Query { get; set; }
        public int Id { get; set; }
    }
}
