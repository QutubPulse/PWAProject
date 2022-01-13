using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Hubs
{
    public class BroadcastHub:Hub
    {
        public async Task SendMessage(string success,string productId, string name, string price, string quantity, string description)
        {
            await Clients.All.SendAsync("ReceiveMessage", new { success= success, productId = productId, name = name, price = price, quantity = quantity, description = description });
        }
    }
}
