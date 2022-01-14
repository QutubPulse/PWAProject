using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using PWAProject.Hubs;
using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async static Task SendMessage(IHubContext<BroadcastHub> moBroadcastHub, ResponseObject foObject, bool flgIsEdit,string fsTemplatepath)
        {
            StringBuilder loScript = new StringBuilder();
            if (flgIsEdit == true)
            {
                loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-id={0}{1}]').length>0){{", foObject.GetType().Name, foObject.inProductId));
                foreach (var obj in foObject.GetType().GetProperties())
                {
                    loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]')){{", foObject.GetType().Name, foObject.inProductId, obj.Name));
                    loScript.Append(string.Format("document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]').textContent=\"{3}\";", foObject.GetType().Name, foObject.inProductId, obj.Name, obj.GetValue(foObject)));
                    loScript.Append("}");
                }
                loScript.Append("}");
            }
            else if (flgIsEdit == false)
            {
                string lsTemplateBody = string.Empty;
                using (StreamReader loStreamReader = new StreamReader(Path.Combine(fsTemplatepath)))
                {
                    lsTemplateBody = loStreamReader.ReadToEnd();
                }
                lsTemplateBody = lsTemplateBody.Replace("{{ObjectName}}", typeof(PWAProject.Models.ResponseObject).Name);
                                
                lsTemplateBody = lsTemplateBody.Replace("{{inProductId.value}}", foObject.inProductId.ToString());
                lsTemplateBody = lsTemplateBody.Replace("{{inProductId}}", Common.GetPropertyName<int?>(() => foObject.inProductId));

                lsTemplateBody = lsTemplateBody.Replace("{{stProductName.value}}", foObject.stProductName);
                lsTemplateBody = lsTemplateBody.Replace("{{stProductName}}", Common.GetPropertyName<string>(() => foObject.stProductName));

                lsTemplateBody = lsTemplateBody.Replace("{{dcPrice.value}}", foObject.dcDiscount.ToString());
                lsTemplateBody = lsTemplateBody.Replace("{{dcPrice}}", Common.GetPropertyName<decimal?>(() => foObject.dcPrice));

                lsTemplateBody = lsTemplateBody.Replace("{{inQuantity.value}}", foObject.inQuantity.ToString());
                lsTemplateBody = lsTemplateBody.Replace("{{inQuantity}}", Common.GetPropertyName<int?>(() => foObject.inQuantity));

                lsTemplateBody = lsTemplateBody.Replace("{{stDescription.value}}", foObject.stDescription.ToString());
                lsTemplateBody = lsTemplateBody.Replace("{{stDescription}}", Common.GetPropertyName<string>(() => foObject.stDescription));
            }
            Console.WriteLine(loScript.ToString());
            await moBroadcastHub.Clients.All.SendAsync("ReceiveMessage", new { uid = foObject.inProductId, timestamp = DateTime.UtcNow.Ticks, script = loScript.ToString() });
        }
    }
}
