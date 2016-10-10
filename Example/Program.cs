using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RatXLib;

namespace Example
{
	class Program
	{
		static void Main(string[] args)
		{
			RatX z = 37*37, t = z / 2;

			for (int i = 0; i < 10; i++)
			{
				t = (z + t*t)/(2*t);
			}

			Console.WriteLine(t);
			Console.ReadLine();
		}
	}
}
