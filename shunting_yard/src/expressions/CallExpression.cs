using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class CallExpression : IExpression
	{
		public IExpression FunctionExpression { get; }

		public IEnumerable<IExpression> Arguments { get; }

		public CallExpression(IExpression functionExpression, IEnumerable<IExpression> arguments)
		{
			FunctionExpression = functionExpression;
			Arguments = arguments.ToArray();
		}

		public void Accept(IExpressionVisitor visitor)
		{
			IExpression[] children = new[] { FunctionExpression }.Concat(Arguments).ToArray();
			visitor.Traverse(() => visitor.Visit(this), children);
		}
	}
}
