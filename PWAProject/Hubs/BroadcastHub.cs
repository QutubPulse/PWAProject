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
            //document.querySelectorAll('[data-pulse-id=Product51]')[0].querySelector('[data-pulse-selecter="stProductName"]')
            ResponseObject loObject = new ResponseObject {
                inProductId = 1,
                stProductName="test",
                dcPrice=300,
                stDescription="Test test"
            };

            StringBuilder loScript = new StringBuilder();
            //loScript.AppendLine("<script type=\"text/Javascript\">");
            foreach (var obj in loObject.GetType().GetProperties())
            {
                loScript.Append(string.Format("document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]').textContent=\"{3}\";", loObject.GetType().Name,loObject.inProductId,obj.Name, obj.GetValue(loObject)));
            }
            //loScript.AppendLine(string.Format("document.querySelectorAll('[data-pulse-id=Product{0}]')[0].querySelector('[data-pulse-selecter=\"{1}\"]').innerHTML", productId, "dcPrice"));
            //loScript.AppendLine(string.Format("document.querySelectorAll('[data-pulse-id=Product{0}]')[0].querySelector('[data-pulse-selecter=\"{1}\"]').innerHTML", productId, "inQuantity"));
            //loScript.AppendLine(string.Format("document.querySelectorAll('[data-pulse-id=Product{0}]')[0].querySelector('[data-pulse-selecter=\"{1}\"]').innerHTML", productId, "stDescription"));
            //loScript.AppendLine("</script>");
            Console.WriteLine(loScript.ToString());
            /*loScript.AppendLine("$('#tr_' + obj.productId).find("td: eq(0)").text(obj.name);
        $('#tr_' + obj.productId).find("td:eq(1)").text(obj.price);
        $('#tr_' + obj.productId).find("td:eq(2)").text(obj.quantity); ");*/
            await Clients.All.SendAsync("ReceiveMessage", new { data= loObject, script=loScript.ToString()});
        }        
    }
}
