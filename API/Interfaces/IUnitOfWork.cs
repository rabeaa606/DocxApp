using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        //  Task<PagedList<PostDto>> GetUsersPosts(PostParams postsParams);
        Task<DocumentDto> GetUsersPosts(DocumentParams documentsParams);
        IUserRepository UserRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}