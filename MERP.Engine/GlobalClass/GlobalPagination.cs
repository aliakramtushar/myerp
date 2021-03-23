using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Engine.GlobalClass
{
    public class GlobalPagination
    {
        public GlobalPagination()
        {
            ListCount = 0;
            List = "";
            ChunkNumber = 100;
            PageNumber = 0;
            Remarks = "";
        }
        public int ListCount { get; set; }
        public string List { get; set; }
        public int ChunkNumber { get; set; }
        public int PageNumber { get; set; }
        public string Remarks { get; set; }
    }
}