using System;

namespace Tarantino.WebManagement.Modules
{
	public abstract class ModuleBase : System.Web.IHttpModule
	{
		protected System.Web.HttpApplication m_context;

		public void Init(System.Web.HttpApplication context)
		{
			m_context = context;
			m_context.PreRequestHandlerExecute += new EventHandler(PreRequestHandlerExecute);
			m_context.BeginRequest += new EventHandler(BeginRequest);
			m_context.AuthenticateRequest += new EventHandler(AuthenticateRequest);
			m_context.AcquireRequestState += new EventHandler(AcquireRequestState);
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