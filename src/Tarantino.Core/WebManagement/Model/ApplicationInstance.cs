using Tarantino.Core.Commons.Model;

namespace Tarantino.Core.WebManagement.Model
{
	public class ApplicationInstance : PersistentObject
	{
		public const string CacheKey = "TarantinoApplicationInstance";

		public const string MachineNameAttribute = "MachineName";
		public const string ApplicationDomainAttribute = "ApplicationDomain";
		public const string UniqueHostHeaderAttribute = "UniqueHostHeader";

		private string _machineName;
		private string _applicationDomain;
		private string _uniqueHostHeader;
		private string _loadBalanceIP;
		private string _cacheRefreshQueryString;
		private string _maintenanceHostHeader;
		private bool _downForMaintenance;
		private bool _availableForLoadBalancing;
		private string _version;

		public string MachineName
		{
			get { return _machineName; }
			set { _machineName = value; }
		}

		public string ApplicationDomain
		{
			get { return _applicationDomain; }
			set { _applicationDomain = value; }
		}

		public string UniqueHostHeader
		{
			get { return _uniqueHostHeader; }
			set { _uniqueHostHeader = value; }
		}

		public string LoadBalanceIP
		{
			get { return _loadBalanceIP; }
			set { _loadBalanceIP = value; }
		}

		public string CacheRefreshQueryString
		{
			get { return _cacheRefreshQueryString; }
			set { _cacheRefreshQueryString = value; }
		}

		public string MaintenanceHostHeader
		{
			get { return _maintenanceHostHeader; }
			set { _maintenanceHostHeader = value; }
		}

		public bool DownForMaintenance
		{
			get { return _downForMaintenance; }
			set { _downForMaintenance = value; }
		}

		public bool AvailableForLoadBalancing
		{
			get { return _availableForLoadBalancing; }
			set { _availableForLoadBalancing = value; }
		}

		public string Version
		{
			get { return _version; }
			set { _version = value; }
		}
	}
}