using System;
using System.Reflection;

namespace TMS.Homework.MVConsole.UI
{
    public class UI
    {
        public void View<T>(T model)
        {
            // // Загружаем библиотеку классов
            //Assembly asm = Assembly.LoadFrom("c:\\Users\\Admin\\source\repos\\TMS.Homework.Nbrb\\Nbrb\\bin\\Debug\\net5.0\\Nbrb.exe");
            //// Находим типы содержащиеся в fontinfo.dll
            //Type[] alltypes = asm.GetTypes();
            //Console.WriteLine("*** Найденые типы ***\n");
            //foreach (Type t1 in alltypes)
            //    Console.WriteLine("-> " + t1.Name);
            //Console.WriteLine();

            //// Выбираем последний класс
            //Type temp = alltypes[alltypes.Length - 1];
            //Console.WriteLine("Используем класс " + temp.Name + "\n");

            //// Отображаем сведения о конструкторах
            //Console.WriteLine("*** Конструкторы ***\n");
            //ConstructorInfo[] ci = temp.GetConstructors();
            //foreach (ConstructorInfo c in ci)
            //{
            //    Console.Write("-> " + c.Name + "(");
            //    // Выводим параметры
            //    ParameterInfo[] p = c.GetParameters();
            //    for (int i = 0; i < p.Length; i++)
            //    {
            //        Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
            //        if (i + 1 < p.Length) Console.Write(", ");
            //    }
            //    Console.Write(")\n\n");
            //}
            //Console.WriteLine("===============================other");

            Type t = typeof(T);
            Console.WriteLine("*** Конструкторы ***\n");
            ConstructorInfo[] ci = t.GetConstructors();
            foreach (ConstructorInfo c in ci)
            {
                Console.Write("-> " + c.Name + "(");
                // Выводим параметры
                ParameterInfo[] p = c.GetParameters();
                for (int i = 0; i < p.Length; i++)
                {
                    Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
                    if (i + 1 < p.Length) Console.Write(", ");
                }
                Console.Write(")\n\n");
            }
            // Получаем коллекцию методов
            MethodInfo[] MArr = t.GetMethods(BindingFlags.DeclaredOnly |
                                                              BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("*** Список методов класса {0} ***\n", model.ToString());

            // Вывести методы
            foreach (MethodInfo m in MArr)
            {
                Console.Write(" --> " + m.ReturnType.Name + " \t" + m.Name + "(");
                // Вывести параметры методов
                ParameterInfo[] p = m.GetParameters();
                for (int i = 0; i < p.Length; i++)
                {
                    Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
                    if (i + 1 < p.Length) Console.Write(", ");
                }
                Console.Write(")\n");
                if (m.Name == "MyMethod")
                {
                    Console.Write("\n вызов метода\n");
                    object[] args = new object[1];
                    args[0] = 9;
                    m.Invoke(model, args);
                }
            }

            Console.WriteLine("\n*** Реализуемые интерфейсы ***\n");
            var im = t.GetInterfaces();
            foreach (Type tp in im)
                Console.WriteLine("--> " + tp.Name);
            Console.WriteLine("\n*** Поля и свойства ***\n");
            FieldInfo[] fieldNames = t.GetFields();
            foreach (FieldInfo fil in fieldNames)
                Console.Write("--> " + fil.ReflectedType.Name + " " + fil.Name + "\n");

           
        }

        // TODO: learn generic type constraints
       // public T Edit<T>(T class1) where T: class, new()
        public T Edit<T>() where T : class, new()
        {
            //return new T();
            T instance = null;

            Type type = typeof(T);

            try
            {
                // Используем позднее связывание
                instance = (T) Activator.CreateInstance(type);
                Console.WriteLine("Объект создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < properties.Length; i++)
            {
                // properties[i].SetValue(i, " ");
                Type propertyType = properties[i].PropertyType;
                Console.WriteLine(properties[i].PropertyType.Name);
                if (propertyType.Name == "Int32") { properties[i].SetValue(instance, 32); }
                if (propertyType.Name == "String") { properties[i].SetValue(instance, "someString"); }
                Console.WriteLine(properties[i].GetValue(instance));
            }

            return instance;
        }
    }
}
