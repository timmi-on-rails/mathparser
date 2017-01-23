using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class FunctionAssignmentExpression : IExpression
	{
		public Identifier FunctionIdentifier { get; }

		public IEnumerable<Identifier> ArgumentNames { get; }

		public IExpression Expression { get; }

		public FunctionAssignmentExpression(Identifier functionIdentifier, IEnumerable<Identifier> arguments, IExpression expression)
		{
			FunctionIdentifier = functionIdentifier;
			Expression = expression;
			ArgumentNames = arguments.ToArray();
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), Expression);
		}
	}
}
