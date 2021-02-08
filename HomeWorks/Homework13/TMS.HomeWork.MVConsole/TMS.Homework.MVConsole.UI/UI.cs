using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace TMS.Homework.MVConsole.UI
{
    public class UI
    {
        public void View<T>(T model)
        {
            Console.WriteLine("Список методов класса : " + model.ToString() + "\n");

            Type t = typeof(T);

            MethodInfo[] arr = t.GetMethods();

            foreach (var item in arr)
            {
                Console.Write(item.ReturnType.Name + "\t" + item.Name + "(");

                ParameterInfo[] parameters = item.GetParameters();

                for (int j = 0; j < parameters.Length; j++)
                {
                    Console.Write(parameters[j].ParameterType.Name + " " + parameters[j].Name);
                }

                Console.Write(")\n");
            }

            Console.WriteLine("\nСписок полей класса : \n");

            FieldInfo[] fieldsInfo = t.GetFields();

            foreach (var fil in fieldsInfo)
                Console.Write(fil.FieldType.Name + " " + fil.Name + "\n");


            Console.WriteLine("\nСписок свойств класса : \n");

            PropertyInfo[] propertiesInfo = t.GetProperties();

            foreach (var fil in propertiesInfo)
                Console.Write(fil.PropertyType.Name + " " + fil.Name + "\n");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Type type = model.GetType();

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance
                                    | BindingFlags.Static
                                    | BindingFlags.Public
                                    | BindingFlags.NonPublic);

            Console.WriteLine("\nСписок полей класса : " + model.ToString() + "\n");

            int i = 0;

            foreach (var field in fields)
            {
                Console.Write($"Field[{i++}] = {field.Name}, type = {field.FieldType.ToString()}, value = ");

                if (field.FieldType.IsClass && field.FieldType.Namespace != "System")
                {
                    object obj = field.GetValue(model);
                    //var newModel = Convert.ChangeType(obj, field.GetType());
                    //View<field.GetType()>(obj);
                }
                else if (field.FieldType.IsArray)
                {
                    Console.Write("{ ");

                    object elementArrays = field.GetValue(model);

                    foreach (var elementArray in ((IEnumerable)elementArrays).Cast<object>())
                    {
                        Console.Write($"{elementArray}, ");
                    }
                    Console.Write("}");
                }
                else
                {
                    Console.Write(field.GetValue(model).ToString());
                }
                Console.WriteLine("");
            }
        }

        // TODO: learn generic type constraints
        public T Edit<T>() where T: class, new()
        {
            return new T();
        }
    }
}
