using System;

namespace PashRuntime
{
	public class Standard
	{
		public static void println(object m)
		{
			Console.WriteLine(m);
		}

		public static void print(object m)
		{
			Console.Write(m);
		}

		public static string readLine() {
			return Console.ReadLine ();
		}

		public static string readKey() {
			return Console.ReadKey ().KeyChar + "";
		}
	}
}

