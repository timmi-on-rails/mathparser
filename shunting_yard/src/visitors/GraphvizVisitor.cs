using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MathParser
{
	class GraphvizVisitor : BottomUpExpressionVisitor
	{
		StringBuilder stringBuilder = new StringBuilder();
		int id = 0;
		Stack<int> idStack = new Stack<int>();

		public override void Visit(CallExpression functionExpression)
		{
			consume("Function Call", functionExpression.Arguments.Count() + 1);
		}

		public override void Visit(TernaryExpression ternaryExpression)
		{
			consume("Ternary", 3);
		}

		public override void Visit(BinaryExpression binaryExpression)
		{
			consume(binaryExpression.BinaryExpressionType.ToString(), 2);
		}

		public override void Visit(ValueExpression valueExpression)
		{
			consume(valueExpression.Value.ToString(), 0);
		}

		public override void Visit(VariableExpression variableExpression)
		{
			consume(variableExpression.Identifier, 0);
		}

		public override void Visit(PrefixExpression prefixExpression)
		{
			consume(prefixExpression.PrefixExpressionType.ToString(), 1);
		}

		private void consume(string nodeName, int count)
		{
			stringBuilder.AppendLine(String.Format("node{0} [ label = \"{1}\" ];", id, nodeName));

			IEnumerable<int> childIds = Enumerable.Range(0, count).Select(x => idStack.Pop()).Reverse();
			foreach (int i in childIds)
			{
				stringBuilder.AppendLine(String.Format("node{0} -> node{1};", id, i));
			}
			idStack.Push(id);
			id++;
		}

		public string GetResult()
		{
			return "digraph G {\n" + stringBuilder.ToString() + "}\n";
		}
	}
}
