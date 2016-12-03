using System;
using shunting_yard;

namespace Demo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string line;
			MathParser mathParser = new MathParser();

			while ((line = Console.ReadLine()) != "exit")
			{
				try
				{
					IExpression e = mathParser.Parse(line);
					Console.WriteLine(e.ToString() + " -> " + e.Evaluate());
				}
				catch (FormatException fe)
				{
					Console.WriteLine(fe.Message);
				}
			}
		}
	}
}
