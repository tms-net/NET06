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
        private Random randomNumber = new Random();
        public void View<T>(T model) //where T : class
        {
            Console.WriteLine("Список методов класса : " + model.ToString() + "\n");

            Type t = typeof(T);

            View(t, model);
        }

        private void View(Type t, object model)
        {
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
                    View(field.FieldType, obj);
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

            SetPropertyOrFields<T>(properties, instance);

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
            SetPropertyOrFields<T>(fields, instance);
            return instance;
        }
        
        private void SetPropertyOrFields<T>(MemberInfo[] properties, T instance)
        {
            //object[] param = { instance };
            //// MethodInfo miGet =  type.GetMethod("GetValue");
            //MethodInfo miGet = type.GetMethods().Single(m => m.Name == "GetValue" && m.GetParameters().Length == 1);
            //// MethodInfo miSet = type.GetMethod("SetValue");
            //MethodInfo miSet = type.GetMethods().Single(m => m.Name == "SetValue" && m.GetParameters().Length == 2);
            //PropertyInfo propertyTypeInfo = null;
            //if (type == typeof(PropertyInfo)) propertyTypeInfo = type.GetProperty("PropertyType");
            //if (type == typeof(FieldInfo)) propertyTypeInfo = type.GetProperty("FieldType");

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                //P property = properties[i];
                //Type propertyType = (Type)propertyTypeInfo.GetValue(property);
                // m.Invoke(model, args);
                // var propertyValue = property.GetValue(instance);

                //var propertyValue = miGet.Invoke(property, param);

                //PropertyInfo propertyNameInfo = type.GetProperty("Name");
                //string propertyName = (string)propertyNameInfo.GetValue(property);
                //var s = propertyValue == null ? "null" : propertyValue;
                //Console.WriteLine($"Property {propertyName}, type {propertyType}, value {s}");

                switch (property)
                {
                    case PropertyInfo pi:
                        pi.SetValue(instance, GetValue(pi.PropertyType));
                        break;
                    case FieldInfo fi:
                        fi.SetValue(instance, GetValue(fi.FieldType));
                        break;
                    default:
                        break;
                }
                // propertyValue = property.GetValue(instance);
                //propertyValue = miGet.Invoke(property, param);
                //s = propertyValue == null ? "null" : propertyValue;
                //Console.WriteLine($"After changing: field {propertyName}, value {s}\n");
            }
        }

        private object GetValue(Type fieldType)
        {
            switch(fieldType)
            {
                case var ft when ft == typeof(int):
                    return randomNumber.Next();
                case var ft when ft == typeof(string):
                    return $"Property_someStringValue";
                case var ft when ft == typeof(double):
                    return randomNumber.NextDouble();
                default:
                    return null;

            }
        }
    }
}

