using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class FunctionExpressionVisitor : EvaluationVisitor
	{
		IEnumerable<string> argNames;
		double[] arguments;

		public FunctionExpressionVisitor(IEnumerable<string> argNames, double[] arguments, FunctionsManager fm, VariablesManager vm) : base(fm, vm)
		{
			this.argNames = argNames;
			this.arguments = arguments;
		}

		public override void Visit(VariableExpression variableExpression)
		{
			if (argNames.Contains(variableExpression.VariableName))
			{
				int idx = argNames.ToList().IndexOf(variableExpression.VariableName);
				_stack.Push(arguments[idx]);
			}
			else
			{
				base.Visit(variableExpression);
			}
		}
	}
}
