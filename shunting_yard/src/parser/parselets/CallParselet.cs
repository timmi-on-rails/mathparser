using System.Collections.Generic;

namespace MathParser
{
	class CallParselet : IInfixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			// TODO look up why some people store the left expression instead of verifying a variable name
			VariableExpression variableExpression = (leftExpression as VariableExpression);

			if (variableExpression == null)
			{
				throw new ParserException("Expected identifier as function name.");
			}

			List<IExpression> arguments = new List<IExpression>();

			if (!tokenStream.Match(TokenType.Rpar))
			{
				do
				{
					arguments.Add(parseExpression(tokenStream));
				} while (tokenStream.Match(TokenType.Comma));

				tokenStream.Consume(TokenType.Rpar);
			}

			return new CallExpression(variableExpression.VariableName, arguments);
		}

		public int Precedence
		{
			get { return Precedences.CALL; }
		}
	}
}
