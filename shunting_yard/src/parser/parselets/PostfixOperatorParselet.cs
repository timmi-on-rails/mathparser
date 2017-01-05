namespace MathParser
{
	class PostfixOperatorParselet : IInfixParselet
	{
		readonly PostfixExpressionType _postfixExpressionType;

		public PostfixOperatorParselet(PostfixExpressionType postfixExpressionType, int precedence)
		{
			_postfixExpressionType = postfixExpressionType;
			Precedence = precedence;
		}

		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			return new PostfixExpression(_postfixExpressionType, leftExpression);
		}

		public int Precedence { get; }
	}
}
