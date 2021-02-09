using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FileLibrary;
using NUnit.Framework;

namespace FileLibraryTests
{
	public class FileServiceTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task SaveAsyncShouldCreateFileWhenItDoesNotExist()
		{
			// arrange
			var fileService = new FileService();
			var testObject = new TestJsonClass();
			var fileThatDoNotExist = Path.Combine(
				Directory.GetCurrentDirectory(), $"{Guid.NewGuid():N}.txt");

			// act
			await fileService.SaveAsync(fileThatDoNotExist, testObject);

			//assert
			Assert.True(File.Exists(fileThatDoNotExist));
		}

		[Test]
		public void SaveAsyncShouldThrowExceptionWhenFileExist()
		{
			//TODO: implementation should be changed

			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void SaveAsyncShouldSaveObjectInJsonFormat()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void SaveAsyncShouldCorrectlyHandleJsonAttributes()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}

		//TODO: Create Tests For LoadAsync<T>()
	}

	public class TestJsonClass
	{
		[JsonIgnore]
		public string PropertyNotToBeSerialized { get; set; }
		public string PropertyToBeSerialized { get; set; }
	}
}