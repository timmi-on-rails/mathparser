using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class CallExpression : IExpression
	{
		public string FunctionName { get; }

		public IEnumerable<IExpression> Arguments { get; }

		public CallExpression(string functionName, IEnumerable<IExpression> arguments)
		{
			FunctionName = functionName;
			Arguments = arguments.ToArray();
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), Arguments.ToArray());
		}
	}
}
