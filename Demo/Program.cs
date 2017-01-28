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
			symbolManager.SetEvalSymbol();

			Console.WriteLine("Enter {0} to quit calculator.", exitCommand);

			while (ReadNextLine())
			{
				try
				{
					Expression expression = mathParser.Parse(currentLine);

					string detail = expression.ToDebug();
					Value result = expression.Evaluate(symbolManager);

					if (result.IsExpression)
					{
						Console.WriteLine("{0} = `{1}`", detail, result);
					}
					else
					{
						Console.WriteLine("{0} = {1}", detail, result);
					}

					expression.ExecuteAssignments(symbolManager);
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
