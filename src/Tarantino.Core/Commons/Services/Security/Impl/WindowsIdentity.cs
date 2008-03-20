using StructureMap;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	[Pluggable(Keys.Default)]
	public class WindowsIdentity : IWindowsIdentity
	{
		public string GetCurrentUsername()
		{
			string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			return username;
		}
	}
}