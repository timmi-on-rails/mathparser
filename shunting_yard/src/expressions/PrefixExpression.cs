namespace shunting_yard
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
			RightOperand.Accept(visitor);
			visitor.Visit(this);
		}
	}
}
