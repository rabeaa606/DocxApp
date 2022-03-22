using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.SignalR
{
    [Authorize]

    public class DocHub : Hub
    {
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _tracker;
        private readonly IUnitOfWork _unitOfWork;

        public DocHub(IMapper mapper, IUnitOfWork unitOfWork, IHubContext<PresenceHub> presenceHub,
           PresenceTracker tracker)
        {
            _unitOfWork = unitOfWork;
            _tracker = tracker;
            _presenceHub = presenceHub;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public async Task ChangeContent(DocEditModel docEditModel)
        {
            var email = Context.User.Getemail();
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);


            var document = _unitOfWork.DocumentRepository.Update(docEditModel);
            //  var connections = await _tracker.GetConnectionsForUser(email);

            await _unitOfWork.Complete();

            await Clients.All.SendAsync("DocumentEdited",
                new { email = email, id = document.Id, content = document.Content, date = DateTime.Now });

            // throw new HubException("DocumentEditedFailed");
        }
    }
}