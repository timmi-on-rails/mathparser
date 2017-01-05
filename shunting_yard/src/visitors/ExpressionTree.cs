namespace MathParser
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
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(_parser.FunctionsManager, _parser.VariablesManager);
			_rootExpression.Accept(evaluationVisitor);
			return evaluationVisitor.GetResult();
		}

		public void Assign()
		{
			AssignVisitor assignVisitor = new AssignVisitor(_parser.FunctionsManager, _parser.VariablesManager);
			_rootExpression.Accept(assignVisitor);
		}

		public string ToDebug()
		{
			PrintVisitor printVisitor = new PrintVisitor();
			_rootExpression.Accept(printVisitor);
			return printVisitor.GetResult();
		}

		public string ToGraphviz()
		{
			GraphvizVisitor graphvizVisitor = new GraphvizVisitor();
			_rootExpression.Accept(graphvizVisitor);
			return graphvizVisitor.GetResult();
		}
	}
}
