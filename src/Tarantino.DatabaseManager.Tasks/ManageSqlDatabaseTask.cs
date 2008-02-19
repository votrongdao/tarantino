using System.IO;
using StructureMap;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using NAnt.Core.Attributes;
using Tarantino.Infrastructure.DatabaseManager.BuildTasks;

namespace Tarantino.DatabaseManager.Tasks
{
	[TaskName("manageSqlDatabase")]
	public class ManageSqlDatabaseTask : Task, ITaskObserver
	{
		private DatabaseAction _action = DatabaseAction.Update;
		private DirectoryInfo _scriptDirectory = new DirectoryInfo(".");
		private string _server = ".";
		private string _database;
		private bool _integratedAuthentication = true;
		private string _username;
		private string _password;
		private string _databaseVersionPropertyName = "usdDatabaseVersion";

		[TaskAttribute("action"), StringValidator(AllowEmpty = false)]
		public DatabaseAction Action
		{
			get { return _action; }
			set { _action = value; }
		}

		[StringValidator(AllowEmpty = false), TaskAttribute("scriptDirectory")]
		public DirectoryInfo ScriptDirectory
		{
			get { return _scriptDirectory; }
			set { _scriptDirectory = value; }
		}

		[TaskAttribute("server"), StringValidator(AllowEmpty = false)]
		public string Server
		{
			get { return _server; }
			set { _server = value; }
		}

		[StringValidator(AllowEmpty = false), TaskAttribute("database", Required = true)]
		public string Database
		{
			get { return _database; }
			set { _database = value; }
		}

		[TaskAttribute("integratedAuthentication"), StringValidator(AllowEmpty = false)]
		public bool IntegratedAuthentication
		{
			get { return _integratedAuthentication; }
			set { _integratedAuthentication = value; }
		}

		[TaskAttribute("username")]
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		[TaskAttribute("password")]
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		protected override void ExecuteTask()
		{
			try
			{
				IDatabaseUpgrader upgrader = ObjectFactory.GetInstance<IDatabaseUpgrader>();
				upgrader.Upgrade(ScriptDirectory.FullName, Server, Database, IntegratedAuthentication, Username, Password, Action, this, _databaseVersionPropertyName);
			}
			catch
			{
				if (FailOnError)
					throw;
			}
		}
	}
}