namespace MathParser
{
	class ValueExpression : IExpression
	{
		public double Value { get; }

		public ValueExpression(double value)
		{
			Value = value;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this));
		}
	}
}
