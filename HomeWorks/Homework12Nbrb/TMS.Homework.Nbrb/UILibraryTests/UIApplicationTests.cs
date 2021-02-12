using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UILibrary;

namespace UILibraryTests
{
	public class UIApplicationTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task ToDoShouldBeLongRunningRoutine() // implemented just for example purposes
		{
			// arrange
			var apiClient = new APILibrary.APIClient();
			var uiApp = new UIApplication(apiClient);
			
			// act
			var uiTask = Task.Run(() => uiApp.ToDo());
			var taskToCheck = await Task.WhenAny(Task.Delay(3000), uiTask);

			//assert
			Assert.AreNotEqual(taskToCheck, uiTask);
		}

		// TODO: try to refactor UIApplication in order to write at lease one test
		// for example  ToDoShouldLoadCurrenciesBeforeUserInput() 
		[Test]
		public async Task ToDoShouldLoadCurrenciesBeforeUserInput() // implemented just for example purposes
		{
			// arrange
			var apiClientMock = new Mock<APILibrary.APIClient>();
			//var apiClient = new APILibrary.APIClient();
			var uiApp = new UIApplication(apiClientMock.Object);


			// act
			var uiTask = Task.Run(() => uiApp.ToDo());
			var taskToCheck = await Task.WhenAny(Task.Delay(1000), uiTask);

			//assert
			//Assert.AreNotEqual(taskToCheck, uiTask);
			apiClientMock.Verify(
				client => client.GetShortCurrenciesAsync(),
				Times.AtLeastOnce());

		}
	}
}