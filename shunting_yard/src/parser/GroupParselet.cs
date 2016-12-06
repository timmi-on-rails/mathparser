using System;
namespace shunting_yard
{
	class GroupParselet : IPrefixParselet
	{
		public IExpression Parse(MathParser parser, Token token)
		{
			IExpression expression = parser.parseExpression();
			parser.consume(TokenType.Rpar);
			return expression;
		}
	}
}
