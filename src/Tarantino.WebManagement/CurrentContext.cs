using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using StructureMap;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.WebManagement
{
	public class CurrentContext
	{
		public static string CacheKey
		{
			get
			{
				string cacheKey = typeof (ApplicationInstance).FullName;
				return cacheKey;
			}
		}

		private static string CurrentAppDomain
		{
			get
			{
				string appDomain = AppDomain.CurrentDomain.FriendlyName.Split('-')[0];
				return appDomain;
			}
		}

		public static ApplicationInstance CurrentApplicationInstance
		{
			get
			{
				ApplicationInstance applicationInstance = null;

				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Items[CacheKey] != null)
					{
						applicationInstance = (ApplicationInstance)HttpContext.Current.Items[CacheKey];
					}
					else if (HttpContext.Current.Cache[CacheKey] != null)
					{
						applicationInstance = (ApplicationInstance)HttpContext.Current.Cache[CacheKey];
						HttpContext.Current.Items[CacheKey] = applicationInstance;
					}
				}

				if (applicationInstance == null)
				{
					try
					{
						IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
						IEnumerable<ApplicationInstance> instanceList = repository.GetByDomainAndMachineName(CurrentAppDomain, Environment.MachineName);
						IList<ApplicationInstance> applicationInstances = new List<ApplicationInstance>(instanceList);

						if (applicationInstances.Count > 0)
						{
							applicationInstance = applicationInstances[0];
						}
						else
						{
							applicationInstance = new ApplicationInstance();
							applicationInstance.MachineName = Environment.MachineName;
							applicationInstance.ApplicationDomain = CurrentAppDomain;
							applicationInstance.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
	
							repository.Save(applicationInstance);
						}
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.ToString());

						applicationInstance = new ApplicationInstance();
						applicationInstance.MachineName = Environment.MachineName;
						applicationInstance.ApplicationDomain = CurrentAppDomain;
						applicationInstance.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
						applicationInstance.DownForMaintenance = true;
					}

					if (HttpContext.Current != null)
					{
						HttpContext.Current.Items[CacheKey] = applicationInstance;
						HttpContext.Current.Cache.Insert(CacheKey, applicationInstance, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero);
					}
				}
				return applicationInstance;
			}
		}
	}
}