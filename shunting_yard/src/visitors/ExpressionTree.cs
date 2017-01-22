namespace MathParser
{
	public class ExpressionTree
	{
		readonly IExpression _rootExpression;

		internal ExpressionTree(IExpression rootExpression)
		{
			_rootExpression = rootExpression;
		}

		public Value Evaluate()
		{
			return Evaluate(null);
		}

		public Value Evaluate(ISymbolManager symbolProvider)
		{
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(symbolProvider);
			_rootExpression.Accept(evaluationVisitor);
			return evaluationVisitor.GetResult();
		}

		/*public void Assign()
		{
			AssignVisitor assignVisitor = new AssignVisitor(_parser.FunctionsManager, _parser.VariablesManager);
			_rootExpression.Accept(assignVisitor);
		}*/

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
