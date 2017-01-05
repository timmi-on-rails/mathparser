namespace MathParser
{
	class VariableAssignmentExpression : IExpression
	{
		public IExpression Expression { get; }

		public string VariableName { get; }

		public VariableAssignmentExpression(string variableName, IExpression expression)
		{
			VariableName = variableName;
			Expression = expression;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), Expression);
		}
	}
}
