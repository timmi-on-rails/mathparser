namespace shunting_yard
{
	class BinaryExpression : IExpression
	{
		public IExpression LeftOperand { get; }

		public IExpression RightOperand { get; }

		public BinaryExpressionType BinaryExpressionType { get; }

		public BinaryExpression(BinaryExpressionType binaryExpressionType, IExpression leftOperand, IExpression rightOperand)
		{		
			BinaryExpressionType = binaryExpressionType;	
			LeftOperand = leftOperand;
			RightOperand = rightOperand;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			LeftOperand.Accept(visitor);
			RightOperand.Accept(visitor);
			visitor.Visit(this);
		}
	}
}
