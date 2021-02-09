using TMS.Homework.MVConsole.Service;
using TMS.Homework.MVConsole.UI;
using TMS.HomeWork.MVConsole.App;

var ui = new UI();
var controller = new Controller(ui, new CsvService());

controller.Run();