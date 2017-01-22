namespace MathParser
{
	class BinaryOperatorParselet : IInfixParselet
	{
		readonly BinaryExpressionType _binaryExpressionType;
		readonly Associativity _associativity;

		public BinaryOperatorParselet(BinaryExpressionType binaryExpressionType, int precedence, Associativity associativity)
		{
			_binaryExpressionType = binaryExpressionType;
			Precedence = precedence;
			_associativity = associativity;
		}

		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			IExpression rightExpression = parseExpression(tokenStream, Precedence + _associativity.ToPrecedenceIncrement());
			return new BinaryExpression(_binaryExpressionType, leftExpression, rightExpression);
		}

		public int Precedence { get; }
	}
}
