namespace shunting_yard
{
	class FunctionExpression : IExpression
	{
		public string FunctionName { get; }

		public IExpression[] Arguments { get; }

		public FunctionExpression(string functionName, IExpression[] arguments)
		{
			FunctionName = functionName;
			Arguments = arguments;
		}

		public void Accept(IExpressionVisitor visitor)
		{
			foreach (IExpression arg in Arguments)
			{
				arg.Accept(visitor);
			}

			visitor.Visit(this);
		}
	}
}
