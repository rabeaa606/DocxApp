using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Entites;
using API.Extensions;
using API.Helpers;
using API.Data;
using System.Linq;

namespace API.Controllers
{

    [Authorize]
    public class DocController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocController(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] DocCreateModel model)
        {
            var email = User.Getemail();
            var documentUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

            var newdocument = new Document
            {
                OwnerId = documentUser.Id,
                OwnerUser = documentUser,
                Content = model.Content
            };

            var newuserdoc = new UserDocument
            {

                UserId = documentUser.Id,
                User = documentUser,
                Email = documentUser.Email,
                DocumentId = newdocument.Id,
                Document = newdocument
            };

            //public ICollection<UserDocument> UserDocuments { get; set; }

            if (newdocument.OwnerUser.Email != email)
                return Unauthorized();

            _unitOfWork.DocumentRepository.AddDocument(newdocument);
            _unitOfWork.DocumentRepository.AddUserDocument(newuserdoc);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to add  Document");
        }
        [HttpPut("edit")]
        public async Task<ActionResult> Edit([FromBody] DocEditModel model)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(User.Getemail());



            var doc = _unitOfWork.DocumentRepository.Update(model);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to share ");
        }
        [HttpPost("share")]
        public async Task<ActionResult> ShareDocument([FromBody] DocShareModel model)
        {
            var User = await _unitOfWork.UserRepository.GetUserByEmailAsync(model.Email);
            if (User == null)
                return BadRequest("This User Not Exist ");
            var doc = await _unitOfWork.DocumentRepository.GetDocument(model.DocumentID);
            var newuser_document = new UserDocument
            {
                DocumentId = doc.Id,
                Document = doc,
                User = User,
                Email = User.Email,
                UserId = User.Id
            };


            _unitOfWork.DocumentRepository.ShareDocument(newuser_document);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to share");
        }


        [HttpPost("get-documents")]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetUserDocument([FromQuery] DocumentParams documentParams)
        {
            documentParams.UserEmail = User.Getemail();
            var currUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(documentParams.UserEmail);
            documentParams.userID = currUser.Id;

            var documents = await _unitOfWork.DocumentRepository.GetUserDocuments(documentParams);

            return Ok(documents);

        }
        [HttpPost("collaborators/{docId}")]
        public ActionResult<string[]> GetDocumentUsers(int docId)
        {
            var documentUsers = _unitOfWork.DocumentRepository.GetDocumentUsers(docId);

            string[] docUser = documentUsers.ToArray();


            return docUser;

        }
    }
}