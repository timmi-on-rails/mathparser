namespace MathParser
{
	class SymbolicExpression : IExpression
	{
		public IExpression Operand { get; }

		public SymbolicExpression(IExpression operand)
		{
			Operand = operand;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			//TODO
			//visitor.Traverse(() => visitor.Visit(this), Operand);
		}
	}
}
