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
        public static string GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }

        public static string GetConstantPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((ConstantExpression)propertyId.Body).Value.ToString();
        }

        public async static Task SendMessage(IHubContext<BroadcastHub> moBroadcastHub, ResponseObject foObject, bool flgIsEdit,string fsTemplatepath)
        {
            StringBuilder loScript = new StringBuilder();
            bool loFirstTime = true;
            int id = 0;
            if (flgIsEdit == true)
            {
                foreach (var obj in foObject.GetType().GetProperties())
                {
                    if (loFirstTime == true)
                    {
                        loScript.Append(string.Format("if(document.querySelector('[data-pulse-id={0}{1}]')!=null){{", foObject.GetType().Name, obj.GetValue(foObject)));
                        id = Convert.ToInt32(obj.GetValue(foObject));
                        loFirstTime = false;
                    }
                    loScript.Append(string.Format("if(document.querySelector('[data-pulse-id={0}{1}]').querySelector('[data-pulse-selecter=\"{2}\"]')){{", foObject.GetType().Name, id, obj.Name));
                    loScript.Append(string.Format("document.querySelector('[data-pulse-id={0}{1}]').querySelector('[data-pulse-selecter=\"{2}\"]').textContent=\"{3}\";", foObject.GetType().Name, id, obj.Name, obj.GetValue(foObject)));
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
                lsTemplateBody = lsTemplateBody.Replace("{{ObjectName}}", foObject.GetType().Name);
                foreach (var obj in foObject.GetType().GetProperties())
                {
                    lsTemplateBody = lsTemplateBody.Replace("{{" + obj.Name + ".value}}", obj.GetValue(foObject).ToString());
                    lsTemplateBody = lsTemplateBody.Replace("{{" + obj.Name + "}}", obj.Name);
                }
                loScript.Append(string.Format("if(document.querySelector('[data-pulse-parentid={0}]')!=null){{", GetConstantPropertyName<string>(() => ResponseObject.stParentId)));
                loScript.Append(string.Format("document.querySelector('[data-pulse-parentid={0}]').insertAdjacentHTML('afterbegin',`{1}`);", GetConstantPropertyName<string>(() => ResponseObject.stParentId), lsTemplateBody));
                loScript.Append("}");
            }
            await moBroadcastHub.Clients.All.SendAsync("ReceiveMessage", new { uid = foObject.inProductId, timestamp = DateTime.UtcNow.Ticks, script = loScript.ToString() });
        }
    }
}
