namespace MathParser
{
	public class MathParser
	{
		public ExpressionTree Parse(string expression)
		{
			using (TokenStream tokenStream = new TokenStream(expression))
			{
				ExpressionParser expressionParser = new ExpressionParser();
				IExpression rootExpression = expressionParser.Parse(tokenStream);

				tokenStream.Consume(TokenType.Eof);

				return new ExpressionTree(rootExpression);
			}
		}
	}
}
