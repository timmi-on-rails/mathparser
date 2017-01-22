using NUnit.Framework;

namespace ExpressionTest
{
	public static class TestExtensions
	{
		public static double Evaluate(this string mathExpression)
		{
			MathParser.MathParser mathParser = new MathParser.MathParser();
			return mathParser.Parse(mathExpression).Evaluate().ToDouble();
		}

		public static void ShouldParseAs(this string mathExpression, string expected)
		{
			MathParser.MathParser mathParser = new MathParser.MathParser();
			Assert.AreEqual(expected, mathParser.Parse(mathExpression).ToDebug());
		}

		public static void ShouldEvaluateTo(this string mathExpression, double expected)
		{
			MathParser.MathParser mathParser = new MathParser.MathParser();
			Assert.AreEqual(expected, mathParser.Parse(mathExpression).Evaluate().ToDouble());
		}
	}
}
