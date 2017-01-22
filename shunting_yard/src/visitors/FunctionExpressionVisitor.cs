using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class FunctionExpressionVisitor : EvaluationVisitor
	{
		IEnumerable<Identifier> argNames;
		Value[] arguments;

		public FunctionExpressionVisitor(IEnumerable<Identifier> argNames, Value[] arguments, ISymbolManager symbolProvider) : base(symbolProvider)
		{
			this.argNames = argNames;
			this.arguments = arguments;
		}

		public override void Visit(VariableExpression variableExpression)
		{
			if (argNames.Contains(variableExpression.Identifier))
			{
				int idx = argNames.ToList().IndexOf(variableExpression.Identifier);
				_evaluationStack.Push(arguments[idx]);
			}
			else
			{
				base.Visit(variableExpression);
			}
		}
	}
}
