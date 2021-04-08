using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.NET06.Lesson26.EFCore
{
	public struct MyStruct
	{
		MyStruct(int fld1)
		{
			Field1 = fld1;
			Field2 = fld1;
		}

		public int Field1;
		public int Field2;
	}

	//----
	//4 byte
	//4 byte

	// ----
	// pointer -> heap

	public class MyClass
	{
		public void MyMethod(ICanSwim canSwim)
		{
			//MyStruct str;
			//MyStruct str2;

			//var s = str + str2;
			////----
			////4 byte
			////4 byte

			//callMethod(str
			//	//----
			//	//4 byte
			//	//4 byte
			//	);
		}
	}

	public interface ICanSwim
	{
		string Swim();

		string Print() => Swim();
	}

	internal interface ICanFly
	{
		void Fly();
	}


	public abstract class Animal
	{
		private int size;

		public abstract string Say();

		public virtual void Print()
		{
		}
	}

	public class ChildClass : Animal, /*ICanSwim,*/ ICanFly
	{
		public void Fly()
		{
			throw new NotImplementedException();
		}

		public override string Say()
		{
			throw new NotImplementedException();
		}

		//void ICanSwim.Swim()
		//{
		//	throw new NotImplementedException();
		//}
	}
}
