using System;
using System.Collections.Generic;

namespace shunting_yard
{
	static class PrefixOperators
	{
		public static IReadOnlyDictionary<TokenType, Operator> Get
		{
			get { return _prefixOperators; }
		}

		private static Dictionary<TokenType, Operator> _prefixOperators = new Dictionary<TokenType, Operator>
		{
			{ TokenType.Minus, new Operator(TokenType.Minus, OperatorType.Prefix, Associativity.Right, 4,
					(mathParser, output) => output.Push(new NegationExpression(output.Pop())))
			}
		};
	}
}
