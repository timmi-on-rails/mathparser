namespace shunting_yard
{
	class PostfixExpression : IExpression
	{
		public PostfixExpressionType PostfixExpressionType { get; }

		public IExpression LeftOperand { get; }

		public void Accept(IExpressionVisitor visitor)
		{
			LeftOperand.Accept(visitor);
			visitor.Visit(this);
		}
	}
}

