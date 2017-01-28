using System;
namespace MathParser
{
	public static class EvaluateSymbols
	{
		public static void SetEvalSymbol(this ISymbolManager symbolManager)
		{
			Function function = (arguments) =>
			{
				Value value = arguments[0];

				return value.ToExpression().Evaluate(symbolManager);
			};

			symbolManager.Set("eval", Value.Function(function));
		}
	}
}
