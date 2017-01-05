namespace MathParser
{
	class BinaryExpression : IExpression
	{
		public BinaryExpressionType BinaryExpressionType { get; }

		public IExpression LeftOperand { get; }

		public IExpression RightOperand { get; }

		public BinaryExpression(BinaryExpressionType binaryExpressionType, IExpression leftOperand, IExpression rightOperand)
		{
			BinaryExpressionType = binaryExpressionType;
			LeftOperand = leftOperand;
			RightOperand = rightOperand;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), LeftOperand, RightOperand);
		}
	}
}
