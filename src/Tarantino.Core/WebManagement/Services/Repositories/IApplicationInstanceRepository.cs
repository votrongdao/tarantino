using System;
using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Repositories
{
	[PluginFamily(ServiceKeys.Default)]
	public interface IApplicationInstanceRepository
	{
		IEnumerable<ApplicationInstance> GetAll();
		IEnumerable<ApplicationInstance> GetByDomainAndMachineName(string applicationDomain, string machineName);
		ApplicationInstance GetById(Guid id);
		void Save(ApplicationInstance instance);
		void Delete(ApplicationInstance instance);
		IEnumerable<ApplicationInstance> GetByHostHeader(string uniqueHostHeader);
	}
}