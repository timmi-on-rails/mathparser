using Tokenizer;

namespace MathParser
{
	class PrefixOperatorParselet : IPrefixParselet
	{
		readonly PrefixExpressionType _prefixExpressionType;
		readonly int _precedence;

		public PrefixOperatorParselet(PrefixExpressionType prefixExpressionType, int precedence)
		{
			_prefixExpressionType = prefixExpressionType;
			_precedence = precedence;
		}

		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			IExpression operand = parseExpression(tokenStream, _precedence);
			return new PrefixExpression(_prefixExpressionType, operand);
		}
	}
}
