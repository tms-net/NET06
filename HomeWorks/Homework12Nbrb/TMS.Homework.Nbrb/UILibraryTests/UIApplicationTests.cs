using System.Threading.Tasks;
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
			var uiApp = new UIApplication();
			
			// act
			var uiTask = Task.Run(() => uiApp.ToDo());
			var taskToCheck = await Task.WhenAny(Task.Delay(3000), uiTask);

			//assert
			Assert.AreNotEqual(taskToCheck, uiTask);
		}

		// TODO: try to refactor UIApplication in order to write at lease one test
		// for example  ToDoShouldLoadCurrenciesBeforeUserInput() 
	}
}