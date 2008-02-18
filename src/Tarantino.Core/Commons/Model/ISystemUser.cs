using System;

namespace Tarantino.Commons.Core.Model
{
	public interface ISystemUser
	{
		string Password { get; set; }
		string EmailAddress { get; set; }
		DateTime? CreatedDate { get; set; }
	}
}