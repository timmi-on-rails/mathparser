using System;
using System.Collections.Generic;

namespace shunting_yard
{
	class CallParselet : IInfixParselet
	{
		public IExpression parse(MathParser parser, IExpression left, Token token)
		{
			// Parse the comma-separated arguments until we hit, ")".
			List<IExpression> args = new List<IExpression>();

			// There may be no arguments at all.
			if (!parser.match(TokenType.Rpar))
			{
				do
				{
					args.Add(parser.parseExpression());
				} while (parser.match(TokenType.Comma));
				parser.consume(TokenType.Rpar);
			}

			return new FunctionExpression((left as VariableExpression).VariableName, args.ToArray());
		}

		public int Precedence
		{
			get { return shunting_yard.Precedence.CALL; }
		}
	}
}
