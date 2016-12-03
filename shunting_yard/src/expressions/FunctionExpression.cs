using System;
using System.Linq;

namespace shunting_yard
{
	class FunctionExpression : IExpression
	{
		private readonly FunctionsManager _functionsManager;

		public string FunctionName { get; }

		public IExpression[] Arguments { get; }

		public FunctionExpression(string functionName, IExpression[] arguments, FunctionsManager functionsManager)
		{	
			FunctionName = functionName;
			Arguments = arguments;

			_functionsManager = functionsManager;
		}

		public bool CanEvaluate()
		{
			return _functionsManager.IsDefined(FunctionName, Arguments.Length)
			&& Arguments.All(expression => expression.CanEvaluate());
		}

		public double Evaluate()
		{
			return _functionsManager.Call(FunctionName, Arguments.Select(expression => expression.Evaluate()).ToArray());
		}

		public override string ToString()
		{
			string argumentList = String.Join(", ", Arguments.Select(expression => expression.ToString()));
			return String.Format("{0}({1})", FunctionName, argumentList);
		}
	}
}
