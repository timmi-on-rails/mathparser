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
			if (Value.IsExpression)

				visitor.Traverse(() => visitor.Visit(this), Value.ToExpression().Expr);
			else
				visitor.Traverse(() => visitor.Visit(this));
		}
	}
}
