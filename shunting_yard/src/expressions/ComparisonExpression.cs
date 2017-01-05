namespace MathParser
{
	class ComparisonExpression : IExpression
	{
		public ComparisonExpressionType ComparisonExpressionType { get; }

		public IExpression LeftOperand { get; }

		public IExpression RightOperand { get; }

		public ComparisonExpression(ComparisonExpressionType comparisonExpressionType, IExpression leftOperand, IExpression rightOperand)
		{
			ComparisonExpressionType = comparisonExpressionType;
			LeftOperand = leftOperand;
			RightOperand = rightOperand;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), LeftOperand, RightOperand);
		}
	}
}
