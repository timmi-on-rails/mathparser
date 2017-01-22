namespace MathParser
{
	class VariableAssignmentExpression : IExpression
	{
		public Identifier Identifier { get; }

		public IExpression Expression { get; }

		public VariableAssignmentExpression(Identifier identifier, IExpression expression)
		{
			Identifier = identifier;
			Expression = expression;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), Expression);
		}
	}
}
