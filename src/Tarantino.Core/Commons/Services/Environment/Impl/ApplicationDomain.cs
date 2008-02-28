using System;
using Tarantino.Core.Commons.Services.Environment;
using StructureMap;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ApplicationDomain : IApplicationDomain
	{
		public string GetBaseFolder()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return baseDirectory;
		}
		
		public string GetName()
		{
			string[] nameParts = AppDomain.CurrentDomain.FriendlyName.Split('-');
			string friendlyName = nameParts[0];
			return friendlyName;
		}
	}
}