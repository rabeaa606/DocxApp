using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entites
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PassworgHash { get; set; }
        public byte[] PassworgSalt { get; set; }
        public ICollection<UserDocument> UserDocuments { get; set; }

    }
}