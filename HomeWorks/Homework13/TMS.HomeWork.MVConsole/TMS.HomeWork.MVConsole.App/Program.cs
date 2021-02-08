using TMS.Homework.MVConsole.UI;

namespace TMS.HomeWork.MVConsole.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            UI ui = new UI();

            Controller controller = new Controller(ui);
        }
    }
}