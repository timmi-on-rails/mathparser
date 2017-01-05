using System.Linq;

namespace MathParser
{
	public class MathParser
	{
		internal VariablesManager VariablesManager { get; }
		internal FunctionsManager FunctionsManager { get; }

		public MathParser()
		{
			VariablesManager = new VariablesManager();
			FunctionsManager = new FunctionsManager();

			FunctionsManager.Define("Max", (double[] arg) => arg.Max());
		}

		public ExpressionTree Parse(string expression)
		{
			TokenStream tokenStream = new TokenStream(expression);

			ExpressionParser expressionParser = new ExpressionParser();
			IExpression res = expressionParser.Parse(tokenStream);

			tokenStream.Consume(TokenType.Eof);

			return new ExpressionTree(this, res);
		}
	}
}
