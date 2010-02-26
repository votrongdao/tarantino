using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using BatchJobs.Core;

namespace BatchJobs.Console
{
	public class Program
	{
		public const string JobagentfactorytypeKey = "JobAgentFactoryType";

		public Func<string> GetFactoryTypeName = () => ConfigurationManager.AppSettings[JobagentfactorytypeKey];

		private static void Main(string[] args)
		{
			Logger.EnsureInitialized();
			try
			{
				new Program().Run(args);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
				var sender = new LogFileToEmailSender();
				
				sender.Send(string.Join(",", args));
				Environment.ExitCode = 100;
			}
		}

		public virtual void Run(string[] args)
		{
			if (args.Length == 0)
			{
				Logger.Fatal(this, "Command Line Instance Name Not Specified");
				System.Console.WriteLine("One of the following instance names must be specified:");
				foreach (var name in Factory().GetInstanceNames())
				{
					System.Console.WriteLine(name);
				}
			}
			else
			{
				Logger.Debug(this, string.Format("Command Line Specified Instance Name: {0}", args[0]));
				IJobAgent jobAgent = Factory().Create(args[0]);
				Logger.Debug(this, "Executing the Job");
				jobAgent.Execute();
			}
		}

		public virtual IJobAgentFactory Factory()
		{
			string unparsedTypename = GetFactoryTypeName();
			string typename;
			string assemblyname;

			try
			{
				Logger.Debug(this, string.Format("Parsing assembly and type names from string \"{0}\"", unparsedTypename));
				assemblyname = unparsedTypename.Split(',')[1].Trim();
				typename = unparsedTypename.Split(',')[0].Trim();
			}
			catch (Exception)
			{
				Logger.Fatal(this, String.Format("Could not parse the typename from the application configuration. configuration value: \"{0}\", configuration key: \"{1}\"", unparsedTypename, JobagentfactorytypeKey));
				throw new InvalidOperationException("Could not parse the typename from the application configuration.");
			}

			Assembly a = null;
			try
			{
				Logger.Debug(this, string.Format("Loading Assembly {0}", assemblyname));
				a = Assembly.Load(assemblyname);
			}
			catch (FileNotFoundException e)
			{
				Logger.Fatal(this, string.Format("Unable to load assembly {0}", assemblyname));
				System.Console.WriteLine(e.Message);
			}
			Type classType = a.GetType(typename);
			Logger.Debug(this, string.Format("Creating instance of {0}", classType));
			return (IJobAgentFactory)Activator.CreateInstance(classType);
		}
	}

	public class DebugerJobAgentFactory : IJobAgentFactory
	{
		public IJobAgent Create(string name)
		{
			System.Console.WriteLine(name);
			return new DebugerJobAgent();
		}

		public IEnumerable<string> GetInstanceNames()
		{
			return new string[] { "Foo", "Bar" };
		}
	}

	public class DebugerJobAgent : IJobAgent
	{
		public void Execute()
		{
			System.Console.WriteLine("Executing");
		}
	}

	public class LogFileToEmailSender
	{
		private const string ToEmailKey = "BatchLogFileToEmail";
		private const string FromEmailKey = "BatchLogFileFromEmail";
		private const string SmtpHostKey = "BatchLogFileSmtpHost";
		private const string FileLocationKey = "BatchLogFileLocation";

		public Func<string> GetToEmail = () => ConfigurationManager.AppSettings[ToEmailKey];
		public Func<string> GetFromEmail = () => ConfigurationManager.AppSettings[FromEmailKey];
		public Func<string> GetSmtpHost = () => ConfigurationManager.AppSettings[SmtpHostKey];
		public Func<string> GetFileLocation = () => ConfigurationManager.AppSettings[FileLocationKey];


		public void Send(string args)
		{
			var message = CreateMessage(args);
			var client = new SmtpClient { Host = GetSmtpHost() };
			client.Send(message);
		}

		public MailMessage CreateMessage(string args)
		{
			var message = new MailMessage(GetFromEmail(), GetToEmail())
			              	{
			              		Subject = String.Format("[{0}] Error on Batch Job", args),
			              		Body = GetFileText()
			              	};
			return message;
		}

		public string GetFileText()
		{
			using (FileStream fs = new FileStream(GetFileLocation(), FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{

					return sr.ReadToEnd();
				}
			}
		}
	}


}