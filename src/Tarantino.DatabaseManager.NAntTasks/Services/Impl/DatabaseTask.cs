using NAnt.Core.Attributes;

namespace Tarantino.DatabaseManager.NAntTasks.Services.Impl
{
	public class DatabaseTask : Task
	{
		private string _database;
		private bool _integratedAuthentication = true;
		private string _password;
		private string _server = ".";
		private string _username;

		[StringValidator(AllowEmpty=false), TaskAttribute("database", Required=true)]
		public string Database
		{
			get { return _database; }
			set { _database = value; }
		}

		[TaskAttribute("integratedAuthentication"), StringValidator(AllowEmpty=false)]
		public bool IntegratedAuthentication
		{
			get { return _integratedAuthentication; }
			set { _integratedAuthentication = value; }
		}

		[TaskAttribute("password")]
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		[TaskAttribute("server"), StringValidator(AllowEmpty=false)]
		public string Server
		{
			get { return _server; }
			set { _server = value; }
		}

		[TaskAttribute("username")]
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}
	}
}