using System.Collections.Generic;

namespace MathParser
{
	public class SymbolManager : ISymbolManager
	{
		readonly Dictionary<Identifier, Value> _symbols = new Dictionary<Identifier, Value>();

		public bool IsSet(Identifier identifier)
		{
			return _symbols.ContainsKey(identifier);
		}

		public Value Get(Identifier identifier)
		{
			return _symbols[identifier];
		}

		public void Set(Identifier identifier, Value value)
		{
			_symbols[identifier] = value;
		}
	}
}
