using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class DocumentDto
    {

        public int Id { get; set; }
        public string OwnerEmail { get; set; }
        public string Content { get; set; }
        public DateTime DocChanged { get; set; }
        public String[] Collaborators { get; set; }
    }
}