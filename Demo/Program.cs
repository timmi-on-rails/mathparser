using System;
using MathParser;

namespace Demo
{
	class MainClass
	{
		const string exitCommand = "exit";
		static string currentLine;

		public static void Main(string[] args)
		{
			MathParser.MathParser mathParser = new MathParser.MathParser();

			SymbolManager symbolManager = new SymbolManager();
			symbolManager.SetTrigonometrySymbols();

			Console.WriteLine("Enter {0} to quit calculator.", exitCommand);

			while (ReadNextLine())
			{
				try
				{
					ExpressionTree expressionTree = mathParser.Parse(currentLine);

					string detail = expressionTree.ToDebug();
					Value result = expressionTree.Evaluate(symbolManager);

					Console.WriteLine("{0} = {1}", detail, result);
				}
				catch (Exception exception)
				{
					Console.WriteLine("error: {0}", exception.Message);
				}
			}
		}

		static bool ReadNextLine()
		{
			Console.Write("> ");
			currentLine = Console.ReadLine();
			return currentLine != exitCommand;
		}
	}
}
