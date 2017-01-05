namespace MathParser
{
	class GroupParselet : IPrefixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			IExpression expression = parseExpression(tokenStream);
			tokenStream.Consume(TokenType.Rpar);
			return new GroupExpression(expression);
		}
	}
}
