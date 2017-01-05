using System;
using System.Diagnostics;
using System.IO;
using MathParser;

namespace Demo
{
	class MainClass
	{
		public static void Main(string[] args)
		{


			string line;
			MathParser.MathParser mathParser = new MathParser.MathParser();

			while ((line = Console.ReadLine()) != "exit")
			{
				try
				{
					ExpressionTree e = mathParser.Parse(line);
					try
					{
						e.Assign();
					}
					catch (Exception e1)
					{
						Console.WriteLine("parse error: " + e1.Message);
					}

					try
					{
						string detail = e.ToDebug();
						Console.Write(detail);
						File.WriteAllText("test.graphviz", e.ToGraphviz());
						Process.Start("/usr/bin/dot", "-Tgif -otest.gif test.graphviz");
						double d = e.Evaluate();
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
