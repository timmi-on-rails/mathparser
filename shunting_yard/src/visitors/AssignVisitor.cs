using System;

namespace MathParser
{/*
	class AssignVisitor : AbstractExpressionVisitor
	{
		FunctionsManager functionsManager;
		VariablesManager variablesManager;

		public AssignVisitor(FunctionsManager functionsManager, VariablesManager variablesManager)
		{
			this.functionsManager = functionsManager;
			this.variablesManager = variablesManager;
		}

		public override void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
			BottomUpEvaluationVisitor evaluationVisitor = new BottomUpEvaluationVisitor(functionsManager, variablesManager);
			variableAssignmentExpression.Expression.Accept(evaluationVisitor);

			variablesManager.Set(variableAssignmentExpression.VariableName, evaluationVisitor.GetResult());
		}

		public override void Visit(FunctionAssignmentExpression functionAssignmentExpression)
		{
			functionsManager.Define(functionAssignmentExpression.FunctionName,
									(args) =>
			{
				FunctionExpressionVisitor fExpEvaluationVisitor = new FunctionExpressionVisitor(functionAssignmentExpression.ArgumentNames, args, functionsManager, variablesManager);
				functionAssignmentExpression.Expression.Accept(fExpEvaluationVisitor);
				return fExpEvaluationVisitor.GetResult();
			});
		}
	}*/
}
