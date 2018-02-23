using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Search
{
    public class BaseSearch
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalCount { set; get; }

    }
}
