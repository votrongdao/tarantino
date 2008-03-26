using System;

namespace Tarantino.Core.Commons.Model
{
	public interface ISystemUser
	{
		string Password { get; set; }
		string EmailAddress { get; set; }
		DateTime? CreatedDate { get; set; }
	}
}