using System;
using shunting_yard;

namespace ExpressionTest
{
	public static class TestExtensions
	{
		public static double Evaluate(this string mathExpression)
		{
			MathParser mathParser = new MathParser();
			return mathParser.Parse(mathExpression).Evaluate();
		}
	}
}

