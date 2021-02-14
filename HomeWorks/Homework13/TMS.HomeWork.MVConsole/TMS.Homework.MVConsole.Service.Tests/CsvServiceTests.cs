using System;
using System.IO;
using System.Linq;
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
			// https://csvlint.io/

			// arrange
			var service = new CsvService();
            var fileName = Guid.NewGuid().ToString("N");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.csv");
            var models = new[] { new { PropertyWithCommas = "Property,With,Commas" } };

			// act
            service.SaveToCSV(models, Directory.GetCurrentDirectory(), fileName);

			//assert

			//Must be magic to working with API here

			Assert.Fail();
		}

        /// <summary>
        /// Method is checking exported file earlier on contain spetific field.
        /// Spetific filed has not public modifier.
        /// </summary>
        [Test]
        public void SaveToCSVShouldSerializeOnlyPublicInstanceProperties()
        {
            // arrange
            var service = new CsvService();
            var fileName = Guid.NewGuid().ToString("N");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.csv");
            var models = new[] { new { FirstName = "Ella", LastName = "Griboedova", Age = 44 } };

            // act
            service.SaveToCSV(models, Directory.GetCurrentDirectory(), fileName);

            //assert
            using var parser = new TextFieldParser(filePath) { TextFieldType = FieldType.Delimited };
            parser.SetDelimiters(",");

            var arrayFields = parser.ReadFields();
            var check = arrayFields!
                .Where(
                    (t, i) =>
                        Equals("Age", (string)arrayFields.GetValue(i))
                ).Any();

            Assert.IsFalse(check);
        }
	}
}