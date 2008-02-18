using Tarantino.Core.Commons.Services.Web;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.UI.Services;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Web
{
	[TestFixture]
	public class WebDataReaderTester
	{
		[Test]
		public void Can_Post_To_Form()
		{
			IWebDataReader reader = new WebDataReader();

			string webData = reader.ReadUrl("http://www.interlacken.com/webdbdev/ch05/formpost.asp", "box1", "Hello World");

			Assert.That(webData.Contains("HELLO WORLD"));
		}
	}
}