using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Support
{
    public interface IWebExecutionContext : IExecutionContext
    {
        T GetCookie<T>(string key) where T : class;
    }


    [Export]
    public class WebApiExecutionContext : IWebExecutionContext
    {
        private IHttpContextAccessor HttpContext;
        public WebApiExecutionContext(IHttpContextAccessor  httpContext)
        {
            this.HttpContext = httpContext;
        }
        #region IExecutionContext Members

        public T GetObject<T>(string key)
        {
            var result = this.HttpContext.HttpContext.Items[key];
            return result != null ? (T)result : default(T);
        }

        public T GetCookie<T>(string key) where T : class
        {
            T data = null;
            var cookie = this.HttpContext.HttpContext.Request.Cookies[key];
            if (cookie != null)
            {
                data = JsonConvert.DeserializeObject<T>(HttpUtility.UrlDecode(cookie));
            }

            return data;
        }

        public void SetObject(string key, object val)
        {
            if (this.HttpContext.HttpContext.Items.ContainsKey(key))
                this.DoRemoveObject(key);
            this.HttpContext.HttpContext.Items.Add(key, val);
        }

        public void RemoveObject(string key)
        {
            this.DoRemoveObject(key);
        }

        public void Dispose()
        {
        }

        #endregion

        protected void DoRemoveObject(string key)
        {
            this.HttpContext.HttpContext.Items.Remove(key);
        }
    }
}
