using System;
using System.Collections.Generic;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.Infrastructure.WebManagement.DataAccess.Repositories
{
	[Pluggable(Keys.Default)]
	public class ApplicationInstanceRepository : IApplicationInstanceRepository
	{
		private readonly IPersistentObjectRepository _objectRepository;

		public ApplicationInstanceRepository(IPersistentObjectRepository objectRepository)
		{
			_objectRepository = objectRepository;
			_objectRepository.ConnectionStringKey = "TarantinoWebManagementConnectionString";
		}

		public IEnumerable<ApplicationInstance> GetAll()
		{
			IEnumerable<ApplicationInstance> instances = _objectRepository.GetAll<ApplicationInstance>();
			return instances;
		}

		public ApplicationInstance GetByMaintenanceHostHeaderAndMachineName(string maintenanceHostHeader, string machineName)
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(ApplicationInstance.MachineNameAttribute, machineName));
			criteria.AddCriterion(new Criterion(ApplicationInstance.MaintenanceHostHeaderAttribute, maintenanceHostHeader));

			ApplicationInstance instance = _objectRepository.FindFirst<ApplicationInstance>(criteria);
			return instance;
		}

		public ApplicationInstance GetById(Guid id)
		{
			ApplicationInstance instance = _objectRepository.GetById<ApplicationInstance>(id);
			return instance;
		}

		public void Save(ApplicationInstance instance)
		{
			_objectRepository.Save(instance);
		}

		public void Delete(ApplicationInstance instance)
		{
			_objectRepository.Delete(instance);
		}

		public IEnumerable<ApplicationInstance> GetByHostHeader(string uniqueHostHeader)
		{
			CriterionSet criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(ApplicationInstance.UniqueHostHeaderAttribute, uniqueHostHeader));
			IEnumerable<ApplicationInstance> instances = _objectRepository.FindAll<ApplicationInstance>(criteria);

			return instances;
		}
	}
}