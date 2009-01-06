using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StructureMap;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Configuration;
using Tarantino.Core.Deployer.Services.Configuration.Impl;
using Tarantino.Infrastructure;
using Environment=System.Environment;

namespace Tarantino.Deployer.Console
{
	public class Program
	{
		private static readonly StringBuilder _buildOutput = new StringBuilder();

		public static void Main(string[] args)
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			if (args.Length != 4)
			{
				OutputUsageAndClose("Supply the correct number of command line arguments");
			}

			string requestedAction = args[0];
			string requestedApplicationName = args[1];
			string requestedEnvironment = args[2];
			string requestedRevision = args[3];

			var applicationRepository = ObjectFactory.GetInstance<IApplicationRepository>();

			Application selectedApplication = applicationRepository.GetByName(requestedApplicationName);

			if (selectedApplication == null)
			{
				OutputUsageAndClose("Specify an application name included in the configuration file 'Tarantino.Deployer.Console.exe.config'");
			}

			var selectedEnvironment = selectedApplication.GetEnvironmentByName(requestedEnvironment);

			if (selectedEnvironment == null)
			{
				OutputUsageAndClose(
					string.Format(
						"Specify an environment included in the configuration file 'Tarantino.Deployer.Console.exe.config' for application '{0}'",
						selectedApplication.Name));
			}

			if (requestedAction == "Deploy")
			{
				int revisionNumber = int.MinValue;

				if (requestedRevision == "CurrentCertified")
				{
					var repository = ObjectFactory.GetInstance<IDeploymentRepository>();
					IEnumerable<Deployment> certifiedDeployments = repository.FindCertified(selectedApplication.Name,
					                                                                        selectedEnvironment.Predecessor);

					if (certifiedDeployments.Count() > 0)
					{
						Deployment lastCertified = certifiedDeployments.OrderByDescending(d => d.CertifiedOn).ElementAt(0);
						revisionNumber = lastCertified.Revision;
					}
				}
				else if (requestedRevision != "Current")
				{
					bool requestedRevisionIsNumber = int.TryParse(requestedRevision, out revisionNumber);

					if (!requestedRevisionIsNumber)
					{
						OutputUsageAndClose(
							"Specify the revision number as either 'Current', 'CurrentCertified', or a valid Subversion revision number");
					}
				}

				var arguments = new StringBuilder("-buildfile:Deployer.build");
				AddArgument(arguments, "application", selectedApplication.Name);
				AddArgument(arguments, "environment", selectedEnvironment.Name);
				AddArgument(arguments, "revision", revisionNumber != int.MinValue ? revisionNumber.ToString() : string.Empty);
				AddArgument(arguments, "url", selectedApplication.Url);
				AddArgument(arguments, "zip.file", selectedApplication.ZipFile);
				AddArgument(arguments, "username", selectedApplication.Username);
				AddArgument(arguments, "password", selectedApplication.Password);

				var caller = new SimpleProcessCaller(@"NAnt\nant.exe", arguments.ToString())
				             	{
				             		StdOutReceived = Caller_OnStdOutReceived,
				             		StdErrorReceived = Caller_OnStdOutReceived
				             	};

				caller.ExecuteProcess();
				var exitCode = caller.ExitCode;

				var recorder = ObjectFactory.GetInstance<IDeploymentRecorder>();
				int revision = recorder.RecordDeployment(selectedApplication.Name, selectedEnvironment.Name, _buildOutput.ToString());

				if (exitCode == 0)
				{
					using (var writer = new StreamWriter("TarantinoDeploymentRevisionNumber.txt"))
					{
						writer.Write(revision);
					}
				}
				else
				{
					Environment.Exit(1);
				}
			}
			else if (requestedAction == "Certify")
			{
				var repository = ObjectFactory.GetInstance<IDeploymentRepository>();
				IEnumerable<Deployment> uncertifiedDeployments = repository.FindSuccessfulUncertified(selectedApplication.Name, selectedEnvironment.Name);

				int revisionNumber;

				bool requestedRevisionIsNumber = int.TryParse(requestedRevision, out revisionNumber);

				if (!requestedRevisionIsNumber)
				{
					OutputUsageAndClose("When certifying a deployment, you must specify a valid Subversion revision number");
				}

				IOrderedEnumerable<Deployment> matchingDeployments =
					uncertifiedDeployments.Where(d => d.Revision == revisionNumber).OrderByDescending(d => d.DeployedOn);

				if (matchingDeployments.Count() > 0)
				{
					Deployment deployment = matchingDeployments.ElementAt(0);
					var certifier = ObjectFactory.GetInstance<IRevisionCertifier>();
					certifier.Certify(deployment);
				}
				else
				{
					OutputUsageAndClose("When certifying a deployment, you must specify a valid deployment that has not already been certified");
				}
			}
			else
			{
				OutputUsageAndClose("Specify either 'Deploy' or 'Certify' for the <Action> argument");
			}
		}

		private static void Caller_OnStdOutReceived(string output)
		{
			System.Console.Out.WriteLine(output);
			_buildOutput.AppendLine(output);
		}

		private static void AddArgument(StringBuilder commandLine, string argument, string argumentValue)
		{
			commandLine.Append(" -D:");
			commandLine.Append(argument);
			commandLine.Append("=\"");
			commandLine.Append(argumentValue);
			commandLine.Append("\"");
		}

		private static void OutputUsageAndClose(string customMessage)
		{
			Out("\n\nINVALID USAGE: Incorrect Command Line Arguments Supplied\n");
			Out("USAGE: Tarantino.Deployer.Console <Action> <Application> <Environment> <Revision>");
			Out("  Action = {Deploy, Certify}");
			Out("  Application = Name of application to deploy");
			Out("  Environment = Environment to deploy application");
			Out("  Revision = {Current, CurrentCertified, <Revision Number>}\n");
			Out("Example 1: Deploy 'PetStore', Revision '4255' to 'Test' environment");
			Out("  Tarantino.Deployer.Console Deploy PetStore Test 4255\n");
			Out("Example 2: Deploy the latest revision of 'PetStore' to 'Test' environment");
			Out("  Tarantino.Deployer.Console Deploy PetStore Test Current\n");
			Out("Example 3: Deploy the latest certified revision of 'PetStore' to 'Test' environment");
			Out("  Tarantino.Deployer.Console Deploy PetStore Test CurrentCertified\n");
			Out("Example 4: Certify 'PetStore', Revision '4255' in the 'Test' environment");
			Out("  Tarantino.Deployer.Console Certify PetStore Test 4255\n\n\n");
			Out("TO CORRECT: " + customMessage);

			Environment.Exit(1);
		}

		private static void Out(string message)
		{
			System.Console.Out.WriteLine(message);
		}
	}
}