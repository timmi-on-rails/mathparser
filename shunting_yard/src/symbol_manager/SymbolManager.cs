using System.Collections.Generic;

namespace MathParser
{
	public class SymbolManager : ISymbolManager
	{
		readonly Dictionary<string, object> _symbols = new Dictionary<string, object>();

		public bool IsSet(string symbolName)
		{
			return _symbols.ContainsKey(symbolName);
		}

		public object Get(string symbolName)
		{
			return _symbols[symbolName];
		}

		public void Set(string symbolName, object value)
		{
			_symbols[symbolName] = value;
		}
	}
}
