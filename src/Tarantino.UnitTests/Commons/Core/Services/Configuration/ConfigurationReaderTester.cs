using System;
using System.Collections.Generic;
using Tarantino.Commons.Core.Services.Configuration;
using Tarantino.Commons.Core.Services.Configuration.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Commons.Core.Services.Configuration
{
	[TestFixture]
	public class ConfigurationReaderTester
	{
		[Test]
		public void Reads_Database_Connection_String()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetConnectionString("DatabaseConnectionString")).Return("conn string");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				string connString = configurationReader.GetConnectionString("DatabaseConnectionString");
				Assert.That(connString, Is.EqualTo("conn string"));
			}

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The database connection string 'DatabaseConnectionString' does not exist in the application configuration file.")]
		public void Throws_Exception_When_Database_Connection_String_Is_Not_Found()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetConnectionString("DatabaseConnectionString")).Return(null);
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				configurationReader.GetConnectionString("DatabaseConnectionString");
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_Application_Setting()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("SampleSetting")).Return("SampleValue");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				string sampleValue = configurationReader.GetRequiredSetting("SampleSetting");
				Assert.That(sampleValue, Is.EqualTo("SampleValue"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_optional_setting_when_setting_is_missing()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("SampleSetting")).Return(null);
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				string sampleValue = configurationReader.GetOptionalSetting("SampleSetting");
				Assert.That(sampleValue, Is.Null);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_optional_setting_when_setting_is_present()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("SampleSetting")).Return("SampleValue");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				string sampleValue = configurationReader.GetOptionalSetting("SampleSetting");
				Assert.That(sampleValue, Is.EqualTo("SampleValue"));
			}

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The application setting 'SampleSetting' does not exist in the application configuration file.")]
		public void Throws_Exception_When_Application_Setting_Is_Not_Found()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();
			
			using(mocks.Record())
			{
				Expect.Call(settings.GetSetting("SampleSetting")).Return(null);
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				configurationReader.GetRequiredSetting("SampleSetting");
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_integer_setting()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("IntegerSetting")).Return("5");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				int setting = configurationReader.GetRequiredIntegerSetting("IntegerSetting");
				Assert.That(setting, Is.EqualTo(5));
			}

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The value for setting 'SampleSetting' ('NonInteger') is not an integer")]
		public void Throws_exception_when_setting_is_not_an_integer()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("SampleSetting")).Return("NonInteger");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				configurationReader.GetRequiredIntegerSetting("SampleSetting");
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_boolean_setting()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("BooleanSetting")).Return("false");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				bool setting = configurationReader.GetRequiredBooleanSetting("BooleanSetting");
				Assert.That(setting, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_missing_optional_boolean_setting()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("BooleanSetting")).Return(null);
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				bool? setting = configurationReader.GetOptionalBooleanSetting("BooleanSetting");
				Assert.That(setting, Is.EqualTo(null));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_optional_boolean_setting()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("BooleanSetting")).Return("true");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				bool? setting = configurationReader.GetOptionalBooleanSetting("BooleanSetting");
				Assert.That(setting, Is.EqualTo(true));
			}

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The value for setting 'SampleSetting' ('NonBoolean') is not a boolean")]
		public void Throws_exception_when_setting_is_not_a_boolean()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration settings = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(settings.GetSetting("SampleSetting")).Return("NonBoolean");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(settings);
				configurationReader.GetRequiredBooleanSetting("SampleSetting");
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_string_array()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration configuration = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(configuration.GetSetting("StringArraySetting")).Return("red, blue, green");
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(configuration);
				IEnumerable<string> settings = configurationReader.GetStringArray("StringArraySetting");
				List<string> settingsList = new List<string>(settings);

				Assert.That(settingsList, Is.EqualTo(new string[]{"red", "blue", "green"}));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Reads_empty_string_array()
		{
			MockRepository mocks = new MockRepository();
			IApplicationConfiguration configuration = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(configuration.GetSetting("StringArraySetting")).Return(null);
			}

			using (mocks.Playback())
			{
				IConfigurationReader configurationReader = new ConfigurationReader(configuration);
				IEnumerable<string> settings = configurationReader.GetStringArray("StringArraySetting");
				List<string> settingsList = new List<string>(settings);

				Assert.That(settingsList, Is.EqualTo(new string[0]));
			}

			mocks.VerifyAll();
		}
	}
}