using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Helpers;

namespace API.Interfaces
{
    public interface IDocumentRepository
    {
        // Task<PagedList<PostDto>> GetUsersPosts(PostParams postsParams);
        void AddDocument(Document doc);
        void AddUserDocument(UserDocument userdoc);
        Task<Document> GetDocument(int id);
        Task<PagedList<DocumentDto>> GetUserDocuments(DocumentParams docParams);
        void ShareDocument(UserDocument user_doc);
        List<string> GetDocumentUsers(int docId);

        Document Update(DocEditModel model);

    }
}