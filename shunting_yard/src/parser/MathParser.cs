using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MathParser
{
	public class MathParser
	{
		public Expression Parse (string expression)
		{
			using (StringReader stringReader = new StringReader (expression)) {
				IEnumerable<Token> tokensWithoutWhiteSpaces = Tokenizer.Tokenize (stringReader)
																	   .Where (token => token.TokenType != TokenType.WhiteSpace);

				using (TokenStream tokenStream = new TokenStream (tokensWithoutWhiteSpaces)) {
					ExpressionParser expressionParser = new ExpressionParser ();
					IExpression rootExpression = expressionParser.Parse (tokenStream);

					tokenStream.Consume (TokenType.EndOfFile);

					return new Expression (rootExpression);
				}
			}
		}
	}
}
