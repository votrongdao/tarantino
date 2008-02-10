using System;

namespace Tarantino.Commons.Core.Model
{
	public class SystemUser : PersistentObject
	{
		public const string EMAIL_ADDRESS = "EmailAddress";
		public const string PASSWORD = "Password";
		public const string CREATED_DATE = "CreatedDate";
	
		private string _emailAddress;
		private string _password;
		private DateTime? _createdDate;

		public SystemUser()
		{
		}

		public SystemUser(string email, string password, DateTime createdDate)
		{
			_emailAddress = email;
			_password = password;
			_createdDate = createdDate;
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public string EmailAddress
		{
			get { return _emailAddress; }
			set { _emailAddress = value; }
		}

		public DateTime? CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}
	}
}