namespace MathParser
{
	class ComparisonParselet : IInfixParselet
	{
		readonly ComparisonExpressionType _comparisonExpressionType;
		readonly Associativity _associativity;

		public ComparisonParselet(ComparisonExpressionType comparisonExpressionType, int precedence, Associativity associativity)
		{
			_comparisonExpressionType = comparisonExpressionType;
			Precedence = precedence;
			_associativity = associativity;
		}

		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			IExpression rightExpression = parseExpression(tokenStream, Precedence + _associativity.ToPrecedence());
			return new ComparisonExpression(_comparisonExpressionType, leftExpression, rightExpression);
		}

		public int Precedence { get; }
	}
}
