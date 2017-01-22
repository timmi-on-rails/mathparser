namespace MathParser
{
	// TODO class for identifiers?
	public interface ISymbolManager
	{
		bool IsSet(string symbolName);
		Value Get(string symbolName);
		void Set(string symbolName, Value value);
	}

	public static class SymbolManagerExtensions
	{
		public static void SetInteger(this ISymbolManager symbolManager, string symbolName, long l)
		{
			symbolManager.Set(symbolName, Value.Integer(l));
		}

		public static void SetFloatingPointNumber(this ISymbolManager symbolManager, string symbolName, double d)
		{
			symbolManager.Set(symbolName, Value.FloatingPointNumber(d));
		}

		public static void SetFunction(this ISymbolManager symbolManager, string symbolName, Function function)
		{
			symbolManager.Set(symbolName, Value.Function(function));
		}

	}
}
