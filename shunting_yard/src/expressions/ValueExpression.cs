namespace MathParser
{
	class ValueExpression : IExpression
	{
		public Value Value { get; }

		public ValueExpression(Value value)
		{
			Value = value;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this));
		}
	}
}
