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

        public static String GetConstantPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((ConstantExpression)propertyId.Body).Value.ToString();
        }

        public async static Task SendMessage(IHubContext<BroadcastHub> moBroadcastHub, ResponseObject foObject, bool flgIsEdit,string fsTemplatepath)
        {
            StringBuilder loScript = new StringBuilder();
            if (flgIsEdit == true)
            {
                int ctr = 0, id = 0;
                foreach (var obj in foObject.GetType().GetProperties())
                {
                    if (ctr == 0)
                    {
                        loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-id={0}{1}]').length>0){{", foObject.GetType().Name, obj.GetValue(foObject)));
                        id = Convert.ToInt32(obj.GetValue(foObject));
                        ctr = 1;
                    }
                    loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]')){{", foObject.GetType().Name, id, obj.Name));
                    loScript.Append(string.Format("document.querySelectorAll('[data-pulse-id={0}{1}]')[0].querySelector('[data-pulse-selecter=\"{2}\"]').textContent=\"{3}\";", foObject.GetType().Name, id, obj.Name, obj.GetValue(foObject)));
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
                loScript.Append(string.Format("if(document.querySelectorAll('[data-pulse-parentid={0}]').length>0){{", GetConstantPropertyName<string>(() => ResponseObject.stParentId)));
                loScript.Append("document.querySelector('[data-pulse-parentid={" + GetConstantPropertyName<string>(() => ResponseObject.stParentId) + "}]').prepend(`" + lsTemplateBody + "`);");
                loScript.Append("}");
            }
            //Console.WriteLine(loScript.ToString());
            await moBroadcastHub.Clients.All.SendAsync("ReceiveMessage", new { uid = foObject.inProductId, timestamp = DateTime.UtcNow.Ticks, script = loScript.ToString() });
        }
    }
}
