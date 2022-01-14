using Microsoft.AspNetCore.SignalR;
using PWAProject.Hubs;
using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PWAProject
{
    public static class Common
    {
        public static String GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }

        public async static Task SendMessage(IHubContext<BroadcastHub> moBroadcastHub, ResponseObject foObject)
        {
            StringBuilder loScript = new StringBuilder();
            if (foObject.flgIsEdit == true)
            {
                foreach (var obj in foObject.GetType().GetProperties())
                {
                    loScript.Append(string.Format("document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]').textContent=\"{3}\";", foObject.GetType().Name, foObject.inProductId, obj.Name, obj.GetValue(foObject)));
                }
            }
            else if (foObject.flgIsEdit == false)
            {

            }
            Console.WriteLine(loScript.ToString());
            await moBroadcastHub.Clients.All.SendAsync("ReceiveMessage", new { data = foObject, script = loScript.ToString() });
        }
    }
}
