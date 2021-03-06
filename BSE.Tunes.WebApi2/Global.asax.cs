﻿using BSE.Tunes.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace BSE.Tunes.WebApi
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			GlobalConfiguration.Configure(WebApiConfig.Register);
			TraceConfig.Register(GlobalConfiguration.Configuration);
		}
	}
}
