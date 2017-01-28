using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	public class MathParser
	{
		public Expression Parse(string expression)
		{
			IEnumerable<Token> tokensWithoutWhiteSpaces = Tokenizer.Tokenize(expression)
																   .Where(token => token.TokenType != TokenType.WhiteSpace);

			using (TokenStream tokenStream = new TokenStream(tokensWithoutWhiteSpaces))
			{
				ExpressionParser expressionParser = new ExpressionParser();
				IExpression rootExpression = expressionParser.Parse(tokenStream);

				tokenStream.Consume(TokenType.EndOfFile);

				return new Expression(rootExpression);
			}
		}
	}
}
