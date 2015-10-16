using ServiceStack;

namespace OpenDocs.Service
{
    [Route("/documents/delete/{Id}")]
    public class DocumentDelete
    {
        public int Id { get; set; }
    }
}
