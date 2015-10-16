using System.Collections.Generic;

namespace OpenDocs.Service.Model.Excel
{
    public class Row
    {
        public int index { get; set; }
        public int height { get; set; }
        public List<Cell> cells { get; set; }
    }
}