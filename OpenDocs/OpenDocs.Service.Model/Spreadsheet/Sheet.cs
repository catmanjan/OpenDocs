using System.Collections.Generic;

namespace OpenDocs.Service.Model.Excel
{
    public class Sheet
    {
        public string name { get; set; }
        public List<Row> rows { get; set; }
        public IEnumerable<int> columns { get; set; }
        public int frozenRows { get; set; }
        public IEnumerable<int> mergedCells { get; set; }
    }
}