using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class FunctionAssignmentExpression : IExpression
	{
		public IExpression Expression { get; }

		public string FunctionName { get; }

		public IEnumerable<string> ArgumentNames { get; }

		public FunctionAssignmentExpression(string functionName, IEnumerable<string> arguments, IExpression expression)
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
