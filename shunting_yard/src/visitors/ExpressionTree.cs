namespace shunting_yard
{
	public class ExpressionTree
	{
		private readonly IExpression _rootExpression;

		MathParser _parser;

		internal ExpressionTree(MathParser parser, IExpression rootExpression)
		{
			_rootExpression = rootExpression;
			_parser = parser;
		}

		public double Evaluate()
		{
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(_parser.FunctionsManager);
			_rootExpression.Accept(evaluationVisitor);
			return evaluationVisitor.GetResult();
		}
	}
}
