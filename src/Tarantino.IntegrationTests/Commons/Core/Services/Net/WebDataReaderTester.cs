using Tarantino.Commons.Core.Services.Net;
using Tarantino.Commons.Core.Services.Net.Impl;
using NUnit.Framework;

namespace Tarantino.IntegrationTests.Commons.Core.Services.Net
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