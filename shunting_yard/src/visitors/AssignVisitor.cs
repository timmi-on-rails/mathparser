using System;

namespace MathParser
{
	class AssignVisitor : BottomUpExpressionVisitor
	{
		readonly ISymbolManager _symbolManager;

		public AssignVisitor(ISymbolManager symbolManager)
		{
			_symbolManager = symbolManager;
		}

		public override void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(_symbolManager);
			variableAssignmentExpression.Expression.Accept(evaluationVisitor);

			_symbolManager.Set(variableAssignmentExpression.VariableName, evaluationVisitor.GetResult());
		}

		public override void Visit(FunctionAssignmentExpression functionAssignmentExpression)
		{
			_symbolManager.Set(functionAssignmentExpression.FunctionName, Value.Function(
									(args) =>
			{
				FunctionExpressionVisitor fExpEvaluationVisitor = new FunctionExpressionVisitor(functionAssignmentExpression.ArgumentNames, args, _symbolManager);
				functionAssignmentExpression.Expression.Accept(fExpEvaluationVisitor);
				return fExpEvaluationVisitor.GetResult();
			}));
		}
	}
}
