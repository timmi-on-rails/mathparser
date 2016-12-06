using System;

namespace shunting_yard
{
	class PrefixOperatorParselet : IPrefixParselet
	{
		private readonly int _precedence;
		private readonly PrefixExpressionType _prefixExpressionType;

		public PrefixOperatorParselet(PrefixExpressionType prefixExpressionType, int precedence)
		{
			_prefixExpressionType = prefixExpressionType;
			_precedence = precedence;
		}

		public IExpression Parse(MathParser parser, Token token)
		{
			IExpression operand = parser.parseExpression(_precedence);
			return new PrefixExpression(_prefixExpressionType, operand);
		}
	}
}

