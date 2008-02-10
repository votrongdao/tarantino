using System.IO;
using Tarantino.DatabaseManager.NAntTasks.Services.Impl;
using NAnt.Core.Attributes;

namespace Tarantino.DatabaseManager.NAntTasks.Services.Impl
{
	[TaskName("manageDatabase")]
	public class ManageDatabaseTask : DatabaseTask
	{
		private DatabaseAction _action = DatabaseAction.Update;
		private DirectoryInfo _scriptDirectory = new DirectoryInfo(".");
		private string _databaseVersionPropertyName = "usdDatabaseVersion";

		protected override void ExecuteTask()
		{
			try
			{
				string directory = ScriptDirectory.FullName;

				IDatabaseUpgrader upgrader = new DatabaseUpgrader();
				upgrader.Upgrade(directory, Server, Database, IntegratedAuthentication, Username, Password, Action, this, DatabaseVersionPropertyName);
			}
			catch
			{
				if (FailOnError)
					throw;
			}
		}

		[TaskAttribute("action"), StringValidator(AllowEmpty=false)]
		public DatabaseAction Action
		{
			get { return _action; }
			set { _action = value; }
		}

		[StringValidator(AllowEmpty=false), TaskAttribute("scriptDirectory")]
		public DirectoryInfo ScriptDirectory
		{
			get { return _scriptDirectory; }
			set { _scriptDirectory = value; }
		}

		[StringValidator(AllowEmpty = false), TaskAttribute("databaseVersionPropertyName")]
		public string  DatabaseVersionPropertyName
		{
			get { return _databaseVersionPropertyName; }
			set { _databaseVersionPropertyName = value; }
		}
	}
}