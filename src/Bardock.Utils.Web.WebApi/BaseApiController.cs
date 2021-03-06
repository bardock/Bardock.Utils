﻿using Bardock.Utils.Logger;
using System.IO;
using System.Web.Http;

namespace Bardock.Utils.Web.WebApi
{
    public abstract class BaseApiController : ApiController
    {
        protected RequestNotifications Notifications
        {
            get { return RequestNotifications.Instance; }
        }

		protected ILog Log 
        {
            get { return Logger.LogManager.Default.GetLog(this); }
		}
    }
}
