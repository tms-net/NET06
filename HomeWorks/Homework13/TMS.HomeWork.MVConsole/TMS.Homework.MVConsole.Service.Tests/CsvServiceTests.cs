using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;
using System;
using System.IO;

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
            var models = new[] { new { PropertyWithCommas = "Property,With,Commas" } };

            // act
            service.SaveToCSV(models, Directory.GetCurrentDirectory(), fileName);

            //assert
            Assert.True(File.Exists(filePath));
            using var parser = new TextFieldParser(filePath) { TextFieldType = FieldType.Delimited };
            parser.SetDelimiters(",");
            string[] line;
            while (!parser.EndOfData)
            {
                line = parser.ReadFields();
                Assert.IsNotNull(line);
                Assert.AreEqual(1, line.Length);
            }
        }

        [Test]
        public void SaveToCSVShouldCreateValidCsvFileFormat()
        {
            // TODO: https://appm.import2.com/csv_file_checker

            // arrange
            var service = new CsvService();
            var fileName = Guid.NewGuid().ToString("N");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.csv");
            TestClass[] models = new TestClass[4];
            for (int i = 0; i <= 3; i++)
            {
                models[i] = new TestClass(i);
            }
            // act
            service.SaveToCSV(models, Directory.GetCurrentDirectory(), fileName);

            //assert
            Assert.True(File.Exists(filePath));
            // act
            string line;

            // Read the file and display it line by line.  
            StreamReader file =
                new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
              if (line == String.Empty) Assert.Fail();

            }
            //assert
            Assert.True(true);
        }

        [Test]
        public void SaveToCSVShouldSerializeOnlyPublicInstanceProperties()
        {
            // arrange
            var service = new CsvService();
            var fileName = Guid.NewGuid().ToString("N");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.csv");
            
            TestClass[] models = new TestClass[4];
            for (int i = 0; i <= 3; i++)
            {
                models[i] = new TestClass(i);
            }
            // act
            service.SaveToCSV(models, Directory.GetCurrentDirectory(), fileName);

            //assert
            Assert.True(File.Exists(filePath));

            // act

            // чтение 1-ой строки
            StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
                break;
            }
            //assert
            Assert.True(line.Contains("PropertyNotSaved")==false);

        }

        class TestClass
        {
           
            private string PropertyNotSaved { get; set; }
            public string PropertSaved1 { get; set; }
            public string PropertSaved2 { get; set; }

            public TestClass(int i)
            {
                PropertyNotSaved = i.ToString();
                PropertSaved1 = (i + 1).ToString();
                PropertSaved2 = (i + 2).ToString();
            }
        }
    }
}