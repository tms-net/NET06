using System;

namespace TMS.Homework.MVConsole.UI
{
    public class UI
    {
        public void View<T>(T model)
        {
            Console.WriteLine(model?.ToString());
        }

        // TODO: learn generic type constraints
        public T Edit<T>() where T: class, new()
        {
            return new T();
        }
    }
}
