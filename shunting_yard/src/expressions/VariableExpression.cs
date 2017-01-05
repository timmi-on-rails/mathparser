namespace MathParser
{
	class VariableExpression : IExpression
	{
		public string VariableName { get; }

		public VariableExpression(string variableName)
		{
			VariableName = variableName;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this));
		}
	}
}
