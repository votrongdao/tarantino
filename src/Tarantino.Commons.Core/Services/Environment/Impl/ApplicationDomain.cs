using System;
using Tarantino.Commons.Core.Services.Environment;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Environment.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ApplicationDomain : IApplicationDomain
	{
		public string GetBaseFolder()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return baseDirectory;
		}
	}
}