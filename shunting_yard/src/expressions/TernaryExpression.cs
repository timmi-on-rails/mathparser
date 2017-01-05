namespace MathParser
{
	class TernaryExpression : IExpression
	{
		public IExpression Condition { get; } //differ between value expressions and conditionexpressions?! or is condition just some value

		public IExpression TrueCase { get; }

		public IExpression FalseCase { get; }

		public TernaryExpression(IExpression condition, IExpression trueCase, IExpression falseCase)
		{
			Condition = condition;
			TrueCase = trueCase;
			FalseCase = falseCase;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Traverse(() => visitor.Visit(this), Condition, TrueCase, FalseCase);
		}
	}
}
