using System.Collections.Generic;

namespace MathParser
{
	public class SymbolManager : ISymbolManager
	{
		readonly Dictionary<string, Value> _symbols = new Dictionary<string, Value>();

		public bool IsSet(string symbolName)
		{
			return _symbols.ContainsKey(symbolName);
		}

		public Value Get(string symbolName)
		{
			return _symbols[symbolName];
		}

		public void Set(string symbolName, Value value)
		{
			_symbols[symbolName] = value;
		}
	}
}
