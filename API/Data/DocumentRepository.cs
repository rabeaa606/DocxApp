using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public void AddDocument(Document doc)
        {
            _context.Documents.Add(doc);

        }

        public void AddUserDocument(UserDocument userdoc)
        {
            _context.UsersDocuments.Add(userdoc);
        }

        public async Task<Document> GetDocument(int id)
        {
            return await _context.Documents
                      .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<DocumentDto>> GetUserDocuments(DocumentParams docParams)
        {

            var user_docs = _context.UsersDocuments
                       .OrderByDescending(m => m.Document.LastChange)
                       .Where(u => u.UserId == docParams.userID)
                         .Take(25)
                         .AsQueryable();

            var docsDto = user_docs.Select(d => new DocumentDto
            {
                Id = d.Document.Id,
                OwnerEmail = d.Document.OwnerUser.Email,
                Content = d.Document.Content,
                DocChanged = d.Document.LastChange,
                //  Collaborators = GetDocumentUsers(d.Document.Id)
            });

            return await PagedList<DocumentDto>.CreateAsync(docsDto,
                                 docParams.PageNumber, docParams.PageSize);

        }

        public List<string> GetDocumentUsers(int docId)
        {
            List<string> users = new List<string>();

            var doc_users = _context.UsersDocuments
                       .Where(u => u.DocumentId == docId)
                         .ToArray();

            foreach (var user in doc_users)
            {
                users.Add(user.Email);
            };


            return users;

        }

        public void ShareDocument(UserDocument user_doc)
        {
            _context.UsersDocuments.Add(user_doc);

        }
        public Document Update(DocEditModel model)
        {
            var currentDoc = _context.Documents.FirstOrDefault(x => x.Id == model.Id);
            currentDoc.Content = model.Content;
            currentDoc.LastChange = DateTime.Now;

            _context.Entry(currentDoc);

            return currentDoc;

        }


    }
}