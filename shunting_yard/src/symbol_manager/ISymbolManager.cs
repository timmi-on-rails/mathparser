namespace MathParser
{
	public delegate object Function(params object[] arguments);

	public interface ISymbolManager
	{
		bool IsSet(string symbolName);
		object Get(string symbolName);
		void Set(string symbolName, object value);
	}

	public static class SymbolManagerExtensions
	{
		public static void SetFunction(this ISymbolManager symbolManager, string symbolName, Function function)
		{
			symbolManager.Set(symbolName, function);
		}
	}
}
