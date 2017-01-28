namespace MathParser
{
	public interface ISymbolManager
	{
		bool IsSet(Identifier identifier);
		Value Get(Identifier identifier);
		void Set(Identifier identifier, Value value);
	}

	public static class SymbolManagerExtensions
	{
		public static void SetInteger(this ISymbolManager symbolManager, Identifier identifier, long l)
		{
			symbolManager.Set(identifier, Value.Integer(l));
		}

		public static void SetDecimal(this ISymbolManager symbolManager, Identifier identifier, double d)
		{
			symbolManager.Set(identifier, Value.Decimal(d));
		}

		public static void SetBoolean(this ISymbolManager symbolManager, Identifier identifier, bool b)
		{
			symbolManager.Set(identifier, Value.Boolean(b));
		}

		public static void SetFunction(this ISymbolManager symbolManager, Identifier identifier, Function function)
		{
			symbolManager.Set(identifier, Value.Function(function));
		}

	}
}
