using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class FunctionAssignmentExpression : IExpression
	{
		public string FunctionName { get; }

		public IEnumerable<Identifier> ArgumentNames { get; }

		public IExpression Expression { get; }

		public FunctionAssignmentExpression(string functionName, IEnumerable<Identifier> arguments, IExpression expression)
		{
			FunctionName = functionName;
			Expression = expression;
			ArgumentNames = arguments.ToArray();
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), Expression);
		}
	}
}
