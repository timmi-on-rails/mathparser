using Tokenizer;

namespace MathParser
{
	class TernaryParselet : IInfixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			IExpression trueExpression = parseExpression(tokenStream);
			tokenStream.Consume(TokenType.Colon);
			IExpression falseExpression = parseExpression(tokenStream, Precedences.CONDITIONAL + Associativity.Right.ToPrecedenceIncrement());
			return new TernaryExpression(leftExpression, trueExpression, falseExpression);
		}

		public int Precedence
		{
			get { return Precedences.CONDITIONAL; }
		}
	}
}
