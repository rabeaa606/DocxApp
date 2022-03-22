using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entites
{
    public class Document
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public AppUser OwnerUser { get; set; }
        public DateTime LastChange { get; set; } = DateTime.Now;
        public string Content { get; set; }
        public ICollection<UserDocument> UserDocuments { get; set; }
    }
}