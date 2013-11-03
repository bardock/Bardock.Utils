using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Bardock.Utils.Web.Extensions;

namespace Bardock.Utils.Web
{
	public class RequestNotifications
	{
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
				if ((HttpContext.Current.Items[KEY] == null)) {
					HttpContext.Current.Items[KEY] = new RequestNotifications();
				}
				return (RequestNotifications)HttpContext.Current.Items[KEY];
			}
		}

		public List<Item> Items { get; set; }

		private RequestNotifications()
		{
			InitItems();
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
					this.Items = JsonConvert.DeserializeObject<List<Item>>(json);
				} 
                catch (Exception ex) 
                {
					Logger.Manager.GetLog(this).Error(ex);
				} 
                finally 
                {
					Response.Cookies.Expire(KEY);
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
            if (this.Items.Count > 0)
            {
                //TODO: Remove Newtonsoft dependency
                string json = JsonConvert.SerializeObject(this.Items);
                Response.Cookies.Add(KEY, json, DateTime.Now.AddMinutes(1));
            }
		}

		public void AddInfo(string message)
		{
			Items.Add(new Item(message, NotificationType.INFO));
		}

		public void AddSuccess(string message)
		{
			Items.Add(new Item(message, NotificationType.SUCCESS));
		}

		public void AddError(string message)
		{
			Items.Add(new Item(message, NotificationType.ERR));
		}

		public void AddWarning(string message)
		{
			Items.Add(new Item(message, NotificationType.WARNING));
		}

		public class Item
		{
			public string Message { get; set; }
			public NotificationType Type { get; set; }

			public Item(string message, NotificationType type)
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