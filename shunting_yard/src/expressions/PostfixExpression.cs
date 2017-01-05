namespace MathParser
{
	class PostfixExpression : IExpression
	{
		public PostfixExpressionType PostfixExpressionType { get; }

		public IExpression LeftOperand { get; }

		public PostfixExpression(PostfixExpressionType postfixExpressionType, IExpression leftOperand)
		{
			PostfixExpressionType = postfixExpressionType;
			LeftOperand = leftOperand;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), LeftOperand);
		}
	}
}
