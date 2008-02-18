using System.Net;
using System.Web;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Net.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class WebDataReader : IWebDataReader
	{
		public string ReadUrl(string url, string parameterName, string parameterValue)
		{
			WebClient client = new WebClient();
			client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			string formData = string.Format("{0}={1}", HttpUtility.UrlEncode(parameterName), HttpUtility.UrlEncode(parameterValue));
			string data = client.UploadString(url, formData);

			return data;
		}
	}
}