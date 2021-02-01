using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using TMS.NET06.Lesson14.Serialization;

namespace TMS.NET06.Lesson14.Reflection
{
    class Program
    {
	    static void Main(string[] args)
        {
	        //Assembly a = Assembly.LoadFrom("MyExe.exe");
			Assembly a = Assembly.GetExecutingAssembly();
	        // Gets the type names from the assembly.
	        Type[] types = a.GetTypes();
	        foreach (Type myType in types)
	        {
		        Console.WriteLine(myType.FullName);
	        }
            Console.WriteLine("Hello World!");

			#region Members
			Type t = typeof(TestClass);
			Console.WriteLine("Listing all the public constructors of the {0} type", t);
			
            // Lists static fields first.
            FieldInfo[] fi = t.GetFields(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Fields");
            PrintMembers(fi);

            // Static properties.
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Properties");
            PrintMembers(pi);

            // Static events.
            EventInfo[] ei = t.GetEvents(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Events");
            PrintMembers(ei);

            // Static methods.
            MethodInfo[] mi = t.GetMethods(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Static Methods");
            PrintMembers(mi);

            // Constructors.
            ConstructorInfo[] ci = t.GetConstructors(BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("// Constructors");
            PrintMembers(ci);

            // Instance fields.
            fi = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);
            Console.WriteLine("// Instance Fields");
            PrintMembers(fi);

            // Instance properites.
            pi = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);
            Console.WriteLine("// Instance Properties");
            PrintMembers(pi);

            // Instance events.
            ei = t.GetEvents(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);
            Console.WriteLine("// Instance Events");
            PrintMembers(ei);

            // Instance methods.
            mi = t.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic
                | BindingFlags.Public);
            Console.WriteLine("// Instance Methods");
            PrintMembers(mi);
            #endregion

            #region Attributes
            System.Reflection.MemberInfo info = typeof(JsonClass);
            foreach (object attrib in info.GetCustomAttributes(true))
            {
	            Console.WriteLine(attrib);
            }
            #endregion

            #region Validation

            PropertyInfo stringLengthField =
	            typeof(string).GetProperty("Length",
		            BindingFlags.Instance | BindingFlags.Public);

            //var length = stringLengthField.GetGetMethod().Invoke(new object(), new object[0]);

            #endregion

            #region Delegate

            Type type = typeof(TestClass);
            PropertyInfo property = type.GetProperty("TestProperty1", BindingFlags.Public | BindingFlags.Instance);
            Func<TestClass, string> getDelegate =
	            (Func<TestClass, string>)Delegate.CreateDelegate(
		            typeof(Func<TestClass, string>),
		            property.GetGetMethod(nonPublic: true));

            Console.WriteLine(getDelegate);

            #endregion

            #region Creation/Invocation

            var instance = (TestClass)Activator.CreateInstance(typeof(TestClass));
            Console.WriteLine(property.GetValue(instance));

            #endregion

            #region Performance

            // by property
            var prop = instance.TestProperty1;

            // by reflection (and cached reflection)
            prop = (string) property.GetValue(instance);

            // by delegate
            prop = getDelegate(instance);

            // by emit
            #endregion

            #region Emit

            // Create an array that specifies the types of the parameters
            // of the dynamic method. This dynamic method has a String
            // parameter and an Integer parameter.
            Type[] helloArgs = { typeof(string), typeof(int) };

            // Create a dynamic method with the name "Hello", a return type
            // of Integer, and two parameters whose types are specified by
            // the array helloArgs. Create the method in the module that
            // defines the String class.
            DynamicMethod hello = new DynamicMethod("Hello",
                typeof(int),
                helloArgs,
                typeof(string).Module);

            // Create an array that specifies the parameter types of the
            // overload of Console.WriteLine to be used in Hello.
            Type[] writeStringArgs = { typeof(string) };
            // Get the overload of Console.WriteLine that has one
            // String parameter.
            MethodInfo writeString = typeof(Console).GetMethod("WriteLine",
                writeStringArgs);

            // Get an ILGenerator and emit a body for the dynamic method,
            // using a stream size larger than the IL that will be
            // emitted.
            ILGenerator il = hello.GetILGenerator(256);
            // Load the first argument, which is a string, onto the stack.
            il.Emit(OpCodes.Ldarg_0);
            // Call the overload of Console.WriteLine that prints a string.
            il.EmitCall(OpCodes.Call, writeString, null);
            // The Hello method returns the value of the second argument;
            // to do this, load the onto the stack and return.
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ret);

            // Create a delegate that represents the dynamic method. This
            // action completes the method. Any further attempts to
            // change the method are ignored.
            Func<string, int, int> hi =
                (Func<string, int, int>)hello.CreateDelegate(typeof(Func<string, int, int>));

            Console.WriteLine(hi("Hello", 5));

            #endregion
        }

	     static void PrintMembers(MemberInfo[] ms)
	    {
		    foreach (MemberInfo m in ms)
		    {
			    Console.WriteLine("{0}{1}", "     ", m);
		    }
		    Console.WriteLine();
	    }
	}

    class TestClass
    {
	    private int testField1;
	    private string testField2;
	    private static string testField3;

	    public string TestProperty1 { get; set; }

	    public void Dump()
	    {
            Console.WriteLine($"{nameof(TestProperty1)}: {TestProperty1}");
	    }
	}

}
