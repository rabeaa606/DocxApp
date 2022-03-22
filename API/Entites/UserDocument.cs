using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entites
{
    public class UserDocument
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public String Email { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}