using Microsoft.AspNetCore.SignalR;
using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWAProject.Hubs
{
    public class BroadcastHub:Hub
    {
        public async Task SendMessage(string fsMsg)
        {           
            //await Clients.All.SendAsync("ReceiveMessage", new { data= loObject, script=loScript.ToString()});
        }        
    }
}
