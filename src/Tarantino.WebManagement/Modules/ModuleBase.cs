using System;

namespace Tarantino.WebManagement.Modules
{
	public abstract class ModuleBase : System.Web.IHttpModule
	{
		protected System.Web.HttpApplication _context;

		public void Init(System.Web.HttpApplication context)
		{
			_context = context;
			_context.PreRequestHandlerExecute += new EventHandler(PreRequestHandlerExecute);
			_context.BeginRequest += new EventHandler(BeginRequest);
			_context.AuthenticateRequest += new EventHandler(AuthenticateRequest);
			_context.AcquireRequestState += new EventHandler(AcquireRequestState);
			Initialized();
		}

		protected virtual void Initialized() { ;}

		public void Dispose()
		{
		}

		protected virtual void AuthenticateRequest(object sender, EventArgs e) { }

		protected abstract void BeginRequest(object sender, EventArgs e);

		protected abstract void AcquireRequestState(object sender, EventArgs e);

		protected virtual void PreRequestHandlerExecute(object sender, EventArgs e) { }
	}
}