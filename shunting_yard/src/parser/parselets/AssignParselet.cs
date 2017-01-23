using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class AssignParselet : IInfixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, IExpression leftExpression)
		{
			IExpression rightExpression = parseExpression(tokenStream, Precedences.ASSIGNMENT + Associativity.Right.ToPrecedenceIncrement());

			if (leftExpression is VariableExpression)
			{
				Identifier identifier = ((VariableExpression)leftExpression).Identifier;
				return new VariableAssignmentExpression(identifier, rightExpression);
			}
			else if (leftExpression is CallExpression)
			{
				CallExpression callExpression = (CallExpression)leftExpression;

				IEnumerable<VariableExpression> arguments = callExpression.Arguments.Concat(new[] { callExpression.FunctionExpression }).Select(argument => (argument as VariableExpression));

				if (arguments.All(arg => arg != null))
				{
					return new FunctionAssignmentExpression(((VariableExpression)callExpression.FunctionExpression).Identifier, arguments.Select(argument => argument.Identifier), rightExpression);
				}
				else
				{
					throw new BadAssignmentException("Every argument in a function assignment must be a variable name.");
				}
			}

			throw new BadAssignmentException("The left hand side of an assignment must either be a function signature or a variable name.");
		}

		public int Precedence { get { return Precedences.ASSIGNMENT; } }
	}
}
