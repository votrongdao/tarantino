using Tarantino.Core.WebManagement.Model;

namespace Tarantino.WebManagement.Handlers
{
	public class Version : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			Write(ApplicationInstance.Current.ApplicationDomain + " " + ApplicationInstance.Current.Version);
		}
	}
}