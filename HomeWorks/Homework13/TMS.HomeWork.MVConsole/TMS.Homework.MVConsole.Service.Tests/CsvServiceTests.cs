using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;

namespace TMS.Homework.MVConsole.Service.Tests
{
	public class CsvServiceTests
	{
		[SetUp]
		public void Setup()
		{
		}
		
		[Test]
		public void SaveToCSVShouldCorrectlySerializePropertiesWnenTheyContainCommas()
		{
			// arrange
			var service = new CsvService();
			var fileName = Guid.NewGuid().ToString("N");
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.csv");
			var models = new[] {new {PropertyWithCommas = "Property,With,Commas"}};

			// act
			service.SaveToCSV(models, Directory.GetCurrentDirectory(), fileName);

			//assert
			Assert.True(File.Exists(filePath));
			using var parser = new TextFieldParser(filePath) {TextFieldType = FieldType.Delimited};
			parser.SetDelimiters(",");
			string[] line;
			while (!parser.EndOfData)
			{
				line = parser.ReadFields();
				Assert.IsNotNull(line);
				Assert.AreEqual(1,line.Length);
			}
		}

		[Test]
		public void SaveToCSVShouldCreateValidCsvFileFormat()
		{
			// TODO: https://appm.import2.com/csv_file_checker

			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void SaveToCSVShouldSerializeOnlyPublicInstanceProperties()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}
	}
}