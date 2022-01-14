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
            try
            {
                //document.querySelectorAll('[data-pulse-id=Product51]')[0].querySelector('[data-pulse-selecter="stProductName"]')
                ResponseObject loObject = new ResponseObject
                {
                    inProductId = 61,
                    stProductName = "test",
                    dcPrice = 300,
                    stDescription = "Test test",
                    inQuantity=12
                };

                StringBuilder loScript = new StringBuilder();
                //loScript.AppendLine("<script type=\"text/Javascript\">");
                loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-id={0}{1}]').length>0){{", loObject.GetType().Name, loObject.inProductId));
                foreach (var obj in loObject.GetType().GetProperties())
                {

                    loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]')){{", loObject.GetType().Name, loObject.inProductId, obj.Name));
                    loScript.Append(string.Format("document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]').textContent=\"{3}\";", loObject.GetType().Name, loObject.inProductId, obj.Name, obj.GetValue(loObject)));
                    loScript.Append("}");
                }
                loScript.Append("}");
                //loScript.AppendLine(string.Format("document.querySelectorAll('[data-pulse-id=Product{0}]')[0].querySelector('[data-pulse-selecter=\"{1}\"]').innerHTML", productId, "dcPrice"));
                //loScript.AppendLine(string.Format("document.querySelectorAll('[data-pulse-id=Product{0}]')[0].querySelector('[data-pulse-selecter=\"{1}\"]').innerHTML", productId, "inQuantity"));
                //loScript.AppendLine(string.Format("document.querySelectorAll('[data-pulse-id=Product{0}]')[0].querySelector('[data-pulse-selecter=\"{1}\"]').innerHTML", productId, "stDescription"));
                //loScript.AppendLine("</script>");
                Console.WriteLine(loScript.ToString());
                /*loScript.AppendLine("$('#tr_' + obj.productId).find("td: eq(0)").text(obj.name);
            $('#tr_' + obj.productId).find("td:eq(1)").text(obj.price);
            $('#tr_' + obj.productId).find("td:eq(2)").text(obj.quantity); ");*/
                await Clients.All.SendAsync("ReceiveMessage", new { data = loObject, script = loScript.ToString() });
            }
            catch(Exception ex)
            {

            }
        }        
    }
}
