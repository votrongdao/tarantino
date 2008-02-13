using System.Data;
using System.IO;
using Tarantino.Commons.Core.Services.DataFileManagement;
using Tarantino.Commons.Core.Services.DataFileManagement.Impl;
using Tarantino.Commons.Core.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;

namespace Tarantino.IntegrationTests.Commons.Core.Services.DataFileManagement
{
	[TestFixture]
	public class ExcelWorkbookReaderTester
	{
		[Test]
		public void Correctly_Reads_Excel_File()
		{
			IResourceFileLocator fileLocator = ObjectFactory.GetInstance<IResourceFileLocator>();

			byte[] excelFileBytes =
				fileLocator.ReadBinaryFile("Tarantino.Commons.Core", "Tarantino.Commons.Core.Services.DataFileManagement.Files.Sample.xls");

			IExcelWorkbookReader reader = new ExcelWorkbookReader();

			DataSet workbook = reader.GetWorkbookData(new MemoryStream(excelFileBytes));

			Assert.That(workbook, Is.Not.Null);

			Assert.That(workbook.Tables.Count, Is.EqualTo(1));
			Assert.That(workbook.Tables["Sample Data"], Is.Not.Null);
			Assert.That(workbook.Tables["Sample Data"].Columns.Count, Is.EqualTo(3));
			Assert.That(workbook.Tables["Sample Data"].Rows.Count, Is.EqualTo(6));
		}
	}
}