using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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


			Console.WriteLine("Listing all types of the {0} assembly", a);
			foreach (Type myType in types)
			{
				Console.WriteLine(myType.FullName);
			}

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine();

			#region Members

			Type t = typeof(TestClass);
			Console.WriteLine("Listing all members of the {0} type", t);

			// Lists static fields first.
			FieldInfo[] fi = t.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Static Fields ---");
			PrintMembers(fi);

			// Static properties.
			PropertyInfo[] pi = t.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Static Properties ---");
			PrintMembers(pi);

			// Static events.
			EventInfo[] ei = t.GetEvents(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Static Events ---");
			PrintMembers(ei);

			// Static methods.
			MethodInfo[] mi = t.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Static Methods ---");
			PrintMembers(mi);

			// Constructors.
			ConstructorInfo[] ci = t.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Constructors ---");
			PrintMembers(ci);

			// Instance fields.
			fi = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Instance Fields ---");
			PrintMembers(fi);

			// Instance properites.
			pi = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Instance Properties ---");
			PrintMembers(pi);

			// Instance events.
			ei = t.GetEvents(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Instance Events ---");
			PrintMembers(ei);

			// Instance methods.
			mi = t.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Instance Methods ---");
			PrintMembers(mi);

			#endregion

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine();

			#region Attributes

			Type jsonClassType = typeof(JsonClass);
			Console.WriteLine("Listing all the attributes of the {0} type", jsonClassType);
			foreach (var attr in jsonClassType.GetCustomAttributes(true))
			{
				Console.WriteLine(attr);
			}
			pi = jsonClassType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			Console.WriteLine("--- Property attributes ---");
			foreach (var p in pi)
			{
				var attrs = p.GetCustomAttributes().ToArray();
				if (attrs.Any())
				{
					Console.WriteLine(" {0}:", p.Name);
					foreach (var attr in attrs)
					{
						Console.WriteLine("    " + attr.GetType());
					}
				}
			}

			#endregion

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine();

			#region Validation

			try
			{
				PropertyInfo stringLengthField = typeof(string).GetProperty("Length", BindingFlags.Instance | BindingFlags.Public);
				var getMethod = stringLengthField.GetGetMethod();
				var pars = new object[0];
				Console.WriteLine("Trying to Invoke {0} method with parameters {1}", getMethod, pars);
				var length = getMethod.Invoke(new object(), pars);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			#endregion

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine();

			#region Delegate

			var type = typeof(TestClass);
			var property = type.GetProperty("TestProperty1", BindingFlags.Public | BindingFlags.Instance);
			var getDelegate = (Func<TestClass, string>)Delegate.CreateDelegate(typeof(Func<TestClass, string>),
				property.GetGetMethod(nonPublic: true));

			Console.WriteLine("Creating delegate for property {0}:", property);
			Console.WriteLine(getDelegate);
			
			#endregion

			#region Creation/Invocation

			var instance = (TestClass)Activator.CreateInstance(typeof(TestClass));
			instance.TestProperty1 = "test property";

			Console.WriteLine("\nInvoking property {0} ({1}) by {2} method for object..", property, instance.TestProperty1, nameof(PropertyInfo.GetValue));
			Console.WriteLine(property.GetValue(instance));

			Console.WriteLine("\nInvoking property {0} ({1}) as delegate {2} for object..", property, instance.TestProperty1, getDelegate);
			Console.WriteLine(getDelegate(instance));
			#endregion

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine();

			#region Performance

			var funcType = typeof(Func<TestClass, string>);
			var getDelegateDynamic = Delegate.CreateDelegate(funcType, property.GetGetMethod(nonPublic: true));

			// by property
			Console.WriteLine("Measuring performance of reflection:");
			var sw = new Stopwatch();

			Console.Write("\nAccess by property: ");
			instance.TestProperty1 = "by property";
			sw.Start();
			var prop = instance.TestProperty1;
			sw.Stop();
			Console.Write("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);
			Console.WriteLine(" ({0})", prop);

			// by reflection (and cached reflection)
			Console.Write("\nAccess by cached reflection: ");
			instance.TestProperty1 = "by cached reflection";
			sw.Restart();
			prop = (string)property.GetValue(instance);
			sw.Stop();
			Console.Write("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);
			Console.WriteLine(" ({0})", prop);

			Console.Write("\nAccess by reflection: ");
			var singleUseInstance = new SingleUseTestClass();
			singleUseInstance.TestProperty1 = "by reflection";
			sw.Restart();
			Type uriType = singleUseInstance.GetType();
			var hostProp = uriType.GetProperty("TestProperty1");
			prop = (string)hostProp.GetValue(singleUseInstance);
			sw.Stop();
			Console.Write("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);
			Console.WriteLine(" ({0})", prop);

			// by delegate
			Console.Write("\nAccess by delegate: ");
			instance.TestProperty1 = "by delegate";
			sw.Restart();
			prop = getDelegate(instance);
			sw.Stop();
			Console.Write("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);
			Console.WriteLine(" ({0})", prop);

			// by dynamic delegate
			Console.Write("\nAccess by dynamic delegate: ");
			instance.TestProperty1 = "by dynamic delegate";
			sw.Restart();
			prop = getDelegateDynamic.DynamicInvoke(instance) as string;
			sw.Stop();
			Console.Write("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);
			Console.WriteLine(" ({0})", prop);

			// by emit
			#region SECRET
			DynamicMethod getProperty = new DynamicMethod("GetTestProperty", typeof(string), new Type[] { typeof(TestClass) }, typeof(TestClass).Module, skipVisibility: true);
			ILGenerator propIl = getProperty.GetILGenerator(256);
			propIl.Emit(OpCodes.Ldarg_0);
			propIl.EmitCall(OpCodes.Call, property.GetGetMethod(true), null);
			propIl.Emit(OpCodes.Ret);
			var propGetter = (Func<TestClass, string>)getProperty.CreateDelegate(typeof(Func<TestClass, string>));
			#endregion
			Console.Write("\nAccess by Emit: ");
			instance.TestProperty1 = "by emit";
			sw.Restart();
			prop = propGetter(instance);
			sw.Stop();
			Console.WriteLine("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);

			// by compiled Expression tree
			#region SECRET
			var paramExpression = Expression.Parameter(typeof(TestClass));
			var propExpression = Expression.Property(paramExpression, property);
			var getPropertyExp = Expression.Lambda(propExpression, paramExpression);
			var compiledDelegate = (Func<TestClass, string>)getPropertyExp.Compile();
			#endregion

			Console.Write("\nAccess by compiled Expression tree: ");
			instance.TestProperty1 = "compiled expression tree";
			sw.Restart();
			prop = compiledDelegate(instance);
			sw.Stop();
			Console.Write("{0} ns", sw.ElapsedTicks * Stopwatch.Frequency / 1000_000_000d);
			Console.WriteLine(" ({0})", prop);
			#endregion

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine();

			#region Emit
			Console.WriteLine("\nExecuting code created by Reflection.Emit");

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
				Console.WriteLine("{0}{1}", "  ", m);
			}
			Console.WriteLine();
		}
	}

	class SingleUseTestClass
	{
		public string TestProperty1 { get; set; }
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
