using System.Collections.Generic;
using Tokenizer;

namespace MathParser
{
	class CallParselet : IInfixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			List<IExpression> arguments = new List<IExpression>();

			if (!tokenStream.Match(TokenType.RightParenthesis))
			{
				do
				{
					arguments.Add(parseExpression(tokenStream));
				} while (tokenStream.Match(TokenType.Comma));

				tokenStream.Consume(TokenType.RightParenthesis);
			}

			return new CallExpression(leftExpression, arguments);
		}

		public int Precedence
		{
			get { return Precedences.CALL; }
		}
	}
}
