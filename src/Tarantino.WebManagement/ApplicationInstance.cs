using System;
using System.Collections;
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
			get { return typeof (ApplicationInstance).FullName; }
		}

		private static string CurrentAppDomain
		{
			get
			{
				return AppDomain.CurrentDomain.FriendlyName.Split('-')[0];
			}
		}

		public static ApplicationInstance Current
		{
			get
			{
				ApplicationInstance a = null;
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Items[CacheKey] != null)
					{
						a = (ApplicationInstance)HttpContext.Current.Items[CacheKey];
					}
					else if (HttpContext.Current.Cache[CacheKey] != null)
					{
						a = (ApplicationInstance)HttpContext.Current.Cache[CacheKey];
						HttpContext.Current.Items[CacheKey] = a;
					}
				}
				if (a == null)
				{
					try
					{
						Hashtable ht = new Hashtable();
						ht.Add("ComputerName", Environment.MachineName);
						ht.Add("ApplicationDomain", CurrentAppDomain);

						IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
						IList<ApplicationInstance> pc = new List<ApplicationInstance>(repository.GetByDomainAndMachineName(CurrentAppDomain, Environment.MachineName));

						if (pc.Count > 0)
						{
							a = pc[0];
						}
						else
						{
							a = new ApplicationInstance();
							a.MachineName = Environment.MachineName;
							a.ApplicationDomain = CurrentAppDomain;
							a.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
	
							repository.Save(a);
						}
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.ToString());
						a = new ApplicationInstance();

						a.MachineName = Environment.MachineName;
						a.ApplicationDomain = CurrentAppDomain;
						a.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
						a.DownForMaintenance = true;
					}

					if (HttpContext.Current != null)
					{
						HttpContext.Current.Items[CacheKey] = a;
						HttpContext.Current.Cache.Insert(CacheKey, a, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero);
					}
				}
				return a;
			}
		}
	}
}