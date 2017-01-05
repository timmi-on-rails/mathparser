namespace MathParser
{
	class PrefixExpression : IExpression
	{
		public PrefixExpressionType PrefixExpressionType { get; }

		public IExpression RightOperand { get; }

		public PrefixExpression(PrefixExpressionType prefixExpressionType, IExpression rightOperand)
		{
			PrefixExpressionType = prefixExpressionType;
			RightOperand = rightOperand;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), RightOperand);
		}
	}
}
