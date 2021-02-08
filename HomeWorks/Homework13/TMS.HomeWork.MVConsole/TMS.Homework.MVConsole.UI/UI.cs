using System;
using System.Linq;
using System.Reflection;

namespace TMS.Homework.MVConsole.UI
{
    public class UI
    {
        private Random randomNumber = new Random();
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
        public T Edit<T>() where T : class, new()
        {
            //return new T();
            T instance = null;

            Type type = typeof(T);

            try
            {
                // Используем позднее связывание
                //instance = (T) Activator.CreateInstance(type);
                instance = new T();
                Console.WriteLine("Объект создан!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine("=============properties=============");
            //for (int i = 0; i < properties.Length; i++)
            //{
            //    PropertyInfo property = properties[i];
            //    Type propertyType = property.PropertyType;
            //    var propertyValue = property.GetValue(instance);

            //    var s = propertyValue == null ? "null" : propertyValue;
            //    Console.WriteLine($"Property {property.Name}, type {propertyType}, value {s}");

            //    switch (property)
            //    {
            //        case PropertyInfo pi when pi.PropertyType == typeof(int):
            //            property.SetValue(instance, randomNumber.Next());
            //            break;
            //        case PropertyInfo pi when pi.PropertyType == typeof(string):
            //            property.SetValue(instance, $"Property_{propertyType.Name}_someStringValue{i.ToString()}");
            //            break;
            //        case PropertyInfo pi when pi.PropertyType == typeof(double):
            //            property.SetValue(instance, randomNumber.NextDouble());
            //            break;
            //        default:
            //            break;
            //    }
            //    propertyValue = property.GetValue(instance);
            //    s = propertyValue == null ? "null" : propertyValue;
            //    Console.WriteLine($"After changing: field {property.Name}, value {s}\n");
            //}

            SetPropertyOrFields<PropertyInfo, T>(properties, instance);

            Console.WriteLine("=============fields============");
            FieldInfo[] fields = type.GetFields();
            //for (int i = 0; i < fields.Length; i++)
            //{
            //    FieldInfo field = fields[i];
            //    Type fieldType = field.FieldType;
            //    var fieldValue = field.GetValue(instance);
            //    var s = fieldValue == null ? "null" : fieldValue;
            //    Console.WriteLine($"Field {field.Name}, type {fieldType}, value {s}");

            //    switch (field)
            //    {
            //        case FieldInfo fi when fi.FieldType == typeof(int):
            //            field.SetValue(instance, randomNumber.Next());
            //            break;
            //        case FieldInfo fi when fi.FieldType == typeof(string):
            //            field.SetValue(instance, $"Field_{fieldType.Name}_someStringValue{i.ToString()}");
            //            break;
            //        case FieldInfo fi when fi.FieldType == typeof(double):
            //            field.SetValue(instance, randomNumber.NextDouble());
            //            break;
            //        case FieldInfo fi when fi.FieldType.IsClass:
            //            fieldValue = Activator.CreateInstance(fieldType);
            //            field.SetValue(instance, fieldValue);
            //            break;
            //        default:
            //            break;
            //    }
            //    fieldValue = field.GetValue(instance);
            //    s = fieldValue == null ? "null" : fieldValue;
            //    Console.WriteLine($"After changing: field {field.Name}, value {s}\n");


            //}
            SetPropertyOrFields<FieldInfo, T>(fields, instance);
            return instance;
        }
        private void SetPropertyOrFields<P, T>(P[] properties, T instance)
        {
            Type type = typeof(P);
            object[] param = { instance };
            // MethodInfo miGet =  type.GetMethod("GetValue");
            MethodInfo miGet = type.GetMethods().Single(m => m.Name == "GetValue" && m.GetParameters().Length == 1);
            // MethodInfo miSet = type.GetMethod("SetValue");
            MethodInfo miSet = type.GetMethods().Single(m => m.Name == "SetValue" && m.GetParameters().Length == 2);
            PropertyInfo propertyTypeInfo = null;
            if (type == typeof(PropertyInfo)) propertyTypeInfo = type.GetProperty("PropertyType");
            if (type == typeof(FieldInfo)) propertyTypeInfo = type.GetProperty("FieldType");

            for (int i = 0; i < properties.Length; i++)
            {
                P property = properties[i];
                Type propertyType = (Type)propertyTypeInfo.GetValue(property);
                // m.Invoke(model, args);
                // var propertyValue = property.GetValue(instance);

                var propertyValue = miGet.Invoke(property, param);

                PropertyInfo propertyNameInfo = type.GetProperty("Name");
                string propertyName = (string)propertyNameInfo.GetValue(property);
                var s = propertyValue == null ? "null" : propertyValue;
                Console.WriteLine($"Property {propertyName}, type {propertyType}, value {s}");

                switch (property)
                {
                    case PropertyInfo pi when pi.PropertyType == typeof(int):
                        // property.SetValue(instance, randomNumber.Next());
                        object[] ps = { instance, randomNumber.Next() };
                        miSet.Invoke(property, ps);
                        break;
                    case PropertyInfo pi when pi.PropertyType == typeof(string):
                        object[] ps1 = { instance, "some" };
                        miSet.Invoke(property, ps1);
                        //property.SetValue(instance, $"Property_{propertyType.Name}_someStringValue{i.ToString()}");
                        break;
                    case PropertyInfo pi when pi.PropertyType == typeof(double):

                        object[] ps2 = { instance, randomNumber.NextDouble() };
                        miSet.Invoke(property, ps2);
                        //property.SetValue(instance, randomNumber.NextDouble());
                        break;
                    default:
                        break;
                }
                // propertyValue = property.GetValue(instance);
                propertyValue = miGet.Invoke(property, param);
                s = propertyValue == null ? "null" : propertyValue;
                Console.WriteLine($"After changing: field {propertyName}, value {s}\n");
            }
        }


    }
}

