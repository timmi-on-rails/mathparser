namespace MathParser
{
	public class ExpressionTree
	{
		readonly IExpression _rootExpression;

		internal ExpressionTree(IExpression rootExpression)
		{
			_rootExpression = rootExpression;
		}

		public object Evaluate()
		{
			return Evaluate(null, null);
		}

		public object Evaluate(IVariableProvider variableProvider, IFunctionProvider functionProvider)
		{
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(functionProvider, variableProvider);
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
