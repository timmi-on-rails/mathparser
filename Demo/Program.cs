using System;
using System.Diagnostics;
using System.IO;
using MathParser;

namespace Demo
{
	public delegate void Funny();
	class MainClass
	{
		public static void digest(Object del)
		{ }



		public static void Test()
		{
		}
		public static void Main(string[] args)
		{
			string line;
			MathParser.MathParser mathParser = new MathParser.MathParser();

			SymbolManager symbolManager = new SymbolManager();
			symbolManager.SetTrigonometrySymbols();

			while ((line = Console.ReadLine()) != "exit")
			{
				try
				{
					ExpressionTree e = mathParser.Parse(line);
					try
					{
						//e.Assign();
					}
					catch (Exception e1)
					{
						Console.WriteLine("parse error: " + e1.Message);
					}

					try
					{
						string detail = e.ToDebug();
						Console.Write(detail);

						object d = e.Evaluate(symbolManager);
						Console.WriteLine(" = " + d);
					}
					catch (Exception e2)
					{
						Console.WriteLine("parse error: " + e2.Message);
					}

				}
				catch (Exception fe)
				{
					Console.WriteLine(fe.Message);
				}
			}
		}
	}
}
