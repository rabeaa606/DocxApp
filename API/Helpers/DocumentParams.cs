using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class DocumentParams : PaginationParams
    {
        public int userID { get; set; }
        public string UserEmail { get; set; }
        public int DocumentID { get; set; }

    }
}