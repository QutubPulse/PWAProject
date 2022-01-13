using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Hubs
{
    public class BroadcastHub:Hub
    {
        public async Task SendMessage(string name, string price, string quantity, string description)
        {
            await Clients.All.SendAsync("ReceiveMessage",new { name = name,price=price,quantity=quantity,description=description });
        }
    }
}
