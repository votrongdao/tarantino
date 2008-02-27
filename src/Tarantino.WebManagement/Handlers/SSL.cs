using System;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;

namespace CallawayGolf.TX.Web.Management
{
	public class SSL : IHttpModule
	{
		public SSL()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IHttpModule Members

		public void Init(HttpApplication context)
		{
			context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
		}

		public void Dispose()
		{
			// Nothing To Do
		}
		#endregion

		private void context_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			HttpApplication myApp = (HttpApplication)sender;
			HttpContext ctx = HttpContext.Current;

			if(ByPassSSL(ctx))
				return;

			//Get list of files to be secured
			NameValueCollection sslFiles = (NameValueCollection)
				ConfigurationSettings.GetConfig("RequiresSSL/RequiresSSL_Files");

			//Get list of paths to be secured
			NameValueCollection sslPaths = (NameValueCollection)
				ConfigurationSettings.GetConfig("RequiresSSL/RequiresSSL_Paths");

			bool RequiresSSL = false;

			//Simple Screen Writes to let the user know what's going on, what we found
			//WriteEntriesToResponse(sslFiles,"Files",ctx);
			//WriteEntriesToResponse(sslPaths,"Paths",ctx);

			string Host = ctx.Request.Url.Host.ToString();
			string File = ctx.Request.Url.PathAndQuery.ToLower();

			//Strip queary string for now
			if(File.IndexOf("?") > -1)
				File = File.Substring(0,File.IndexOf("?"));

			//First check the paths
			RequiresSSL = IsFileInrequiredCollection(sslPaths, ctx, File);

			//Now check the files, only if path doesn't require SSL
			if(RequiresSSL == false)
				RequiresSSL = IsFileInrequiredCollection(sslFiles, ctx, File);

			if(RequiresSSL && ! ctx.Request.IsSecureConnection)
				ctx.Response.Redirect(ctx.Request.Url.ToString().ToLower().Replace("http:","https:"));
			
			if( ctx.Request.IsSecureConnection && !RequiresSSL )
				ctx.Response.Redirect(ctx.Request.Url.ToString().ToLower().Replace("https:","http:"));
		}

		private bool ByPassSSL(HttpContext ctx)
		{
			return (ctx.Request.Cookies["BypassSSL"]!=null && ctx.Request.Cookies["BypassSSL"].Value=="true");
		}

		private bool IsFileInrequiredCollection(NameValueCollection nv, HttpContext ctx, string filename)
		{
			bool RequiresSSL = false;        
			foreach(string key in nv.Keys)
			{
				if(filename.StartsWith(key.ToLower()) || filename.EndsWith(key.ToLower()))
				{
					RequiresSSL = true;
					break;
				}
			}
			return RequiresSSL;
		}
    
		private void WriteEntriesToResponse(NameValueCollection nv, string title, HttpContext ctx)
		{
			ctx.Response.Write("<HR><B>Secured " + title + " Section</B><BR>");
			foreach(string key in nv.Keys)
			{
				ctx.Response.Write(key + "<BR>");
			}
		}
	}
}