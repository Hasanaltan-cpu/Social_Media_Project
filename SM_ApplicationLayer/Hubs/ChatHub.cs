using Microsoft.AspNetCore.SignalR;
using SM_DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Hubs
{
   public  class ChatHub:Hub
    {
        public async Task SendMessage(Message message) => await Clients.All.SendAsync("recieveMessage", message);


    }
}
