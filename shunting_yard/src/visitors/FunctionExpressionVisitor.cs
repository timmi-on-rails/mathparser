﻿using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class FunctionExpressionVisitor : EvaluationVisitor
	{
		IEnumerable<string> argNames;
		Value[] arguments;

		public FunctionExpressionVisitor(IEnumerable<string> argNames, Value[] arguments, ISymbolManager symbolProvider) : base(symbolProvider)
		{
			this.argNames = argNames;
			this.arguments = arguments;
		}

		public override void Visit(VariableExpression variableExpression)
		{
			if (argNames.Contains(variableExpression.VariableName))
			{
				int idx = argNames.ToList().IndexOf(variableExpression.VariableName);
				_evaluationStack.Push(arguments[idx]);
			}
			else
			{
				base.Visit(variableExpression);
			}
		}
	}
}
