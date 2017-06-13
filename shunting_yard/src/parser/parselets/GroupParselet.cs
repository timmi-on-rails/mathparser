using Tokenizer;

namespace MathParser
{
	class GroupParselet : IPrefixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			IExpression expression = parseExpression(tokenStream);
			tokenStream.Consume(TokenType.RightParenthesis);
			return new GroupExpression(expression);
		}
	}
}
