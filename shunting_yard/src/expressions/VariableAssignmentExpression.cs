namespace shunting_yard
{
	class VariableAssignmentExpression : IExpression
	{
		private readonly VariablesManager _variablesManager;

		public IExpression Expression { get; }

		public string VariableName { get; }

		public VariableAssignmentExpression(string variableName, IExpression expression, VariablesManager variablesManager)
		{
			VariableName = variableName;
			Expression = expression;
			_variablesManager = variablesManager;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			Expression.Accept(visitor);
			visitor.Visit(this);
		}
	}
}
