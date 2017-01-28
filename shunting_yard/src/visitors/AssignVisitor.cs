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

			_symbolManager.Set(variableAssignmentExpression.Identifier, evaluationVisitor.GetResult());
		}

		public override void Visit(FunctionAssignmentExpression functionAssignmentExpression)
		{
			_symbolManager.Set(functionAssignmentExpression.FunctionIdentifier, GetFunction(functionAssignmentExpression, _symbolManager));
		}

		public static Value GetFunction(FunctionAssignmentExpression functionAssignmentExpression, ISymbolManager symbolManager)
		{
			return Value.Function((args) =>
			{
				FunctionExpressionVisitor fExpEvaluationVisitor = new FunctionExpressionVisitor(functionAssignmentExpression.ArgumentNames, args, symbolManager);
				functionAssignmentExpression.Expression.Accept(fExpEvaluationVisitor);
				return fExpEvaluationVisitor.GetResult();
			});
		}
	}
}
