using Bardock.Utils.Web.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Web;

namespace Bardock.Utils.Web
{
    public class RequestNotifications
    {
        public static DefaultContractResolver ContractResolver { get; set; }

        private static readonly string KEY = typeof(RequestNotifications).FullName;

        private static HttpRequest Request { get { return HttpContext.Current.Request; } }

        private static HttpResponse Response { get { return HttpContext.Current.Response; } }

        /// <summary>
        /// Singleton per-request
        /// </summary>
        public static RequestNotifications Instance
        {
            get
            {
                if (HttpContext.Current.Items[KEY] == null)
                    HttpContext.Current.Items[KEY] = new RequestNotifications();
                return (RequestNotifications)HttpContext.Current.Items[KEY];
            }
        }

        public List<Item> Items { get; set; }

        private RequestNotifications()
        {
            ContractResolver = new DefaultContractResolver();
            InitItems();
        }

        private JsonSerializerSettings BuildJsonSettings()
        {
            return new JsonSerializerSettings() { ContractResolver = ContractResolver };
        }

        /// <summary>
        /// Tries deserialize persisted state
        /// </summary>
        private void InitItems()
        {
            string json = Request.Cookies.GetValue(KEY);
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    this.Items = JsonConvert.DeserializeObject<List<Item>>(json, BuildJsonSettings());
                }
                catch (Exception ex)
                {
                    Logger.LogManager.Default.GetLog(this).Error(ex);
                }
                finally
                {
                    if (!Response.AreHeadersWritten())
                    {
                        Response.Cookies.Expire(KEY);
                    }
                }
                return;
            }
            this.Items = new List<Item>();
        }

        /// <summary>
        /// Stores current notifications in cookies, so following request can access them
        /// </summary>
        public void PersistState()
        {
            if (this.Items.Count > 0 && !Response.AreHeadersWritten())
            {
                //TODO: Remove Newtonsoft dependency
                string json = JsonConvert.SerializeObject(this.Items, BuildJsonSettings());
                Response.Cookies.Add(KEY, json, DateTime.Now.AddMinutes(1));
            }
        }

        public void Add(object data, NotificationType type)
        {
            Items.Add(new Item(data, type));
        }

        public void AddInfo(object message)
        {
            Add(message, NotificationType.INFO);
        }

        public void AddSuccess(object message)
        {
            Add(message, NotificationType.SUCCESS);
        }

        public void AddError(object message)
        {
            Add(message, NotificationType.ERR);
        }

        public void AddWarning(object message)
        {
            Add(message, NotificationType.WARNING);
        }

        public class Item
        {
            public object Message { get; set; }

            public NotificationType Type { get; set; }

            public Item(object message, NotificationType type)
            {
                this.Message = message;
                this.Type = type;
            }
        }

        public enum NotificationType
        {
            INFO,
            SUCCESS,
            ERR,
            WARNING
        }
    }
}