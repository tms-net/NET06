using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.NET06.Lesson13.AsyncAwait
{
	class Program
	{
		private static Task<int> Main(string[] args)
		{
            Console.WriteLine("Hello world!!");
            return Task.FromResult(0);
		}
	}
}

