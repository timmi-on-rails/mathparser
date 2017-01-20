using System;

namespace MathParser
{
	public static class TrigonometrySymbols
	{
		public static void SetTrigonometrySymbols(this ISymbolManager symbolManager)
		{
			symbolManager.Define("sin", Math.Sin);
			symbolManager.Define("cos", Math.Cos);
			symbolManager.Define("tan", Math.Tan);
		}

		static void Define(this ISymbolManager symbolManager, string symbolName, Func<double, double> method)
		{
			Function function = (arguments) =>
			{
				if (arguments.Length != 1 || !(arguments[0] is double))
				{
					string message = String.Format("Function {0} expects exactly one argument of type number. Given {1} arguments.",
												   symbolName, arguments.Length);
					throw new EvaluationException(message);
				}
				double x = (double)arguments[0];

				return method(x);
			};

			symbolManager.SetFunction(symbolName, function);
		}
	}
}
