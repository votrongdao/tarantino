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
	}
}