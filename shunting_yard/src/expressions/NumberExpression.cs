namespace MathParser
{
	class NumberExpression : IExpression
	{
		public double Value { get; }

		public NumberExpression(double value)
		{
			Value = value;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this));
		}
	}
}
