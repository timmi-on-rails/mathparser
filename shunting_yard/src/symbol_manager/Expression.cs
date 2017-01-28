namespace MathParser
{
	public class Expression
	{
		internal IExpression Expr { get; }

		internal Expression(IExpression expression)
		{
			Expr = expression;
		}

		public Value Evaluate()
		{
			return Evaluate(null);
		}

		public Value Evaluate(ISymbolManager symbolManager)
		{
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(symbolManager);
			Expr.Accept(evaluationVisitor);
			return evaluationVisitor.GetResult();
		}

		public void ExecuteAssignments(ISymbolManager symbolManager)
		{
			AssignVisitor assignVisitor = new AssignVisitor(symbolManager);
			Expr.Accept(assignVisitor);
		}

		public override string ToString()
		{
			return ToDebug();
		}

		public string ToDebug()
		{
			PrintVisitor printVisitor = new PrintVisitor();
			Expr.Accept(printVisitor);
			return printVisitor.GetResult();
		}

		/*public string ToPrettyString()
		{
			// TODO decide whether to use new visitor or maybe take input and place white spaces?!
			return ToDebug();
		}*/

		public string ToGraphviz()
		{
			GraphvizVisitor graphvizVisitor = new GraphvizVisitor();
			Expr.Accept(graphvizVisitor);
			return graphvizVisitor.GetResult();
		}
	}
}
