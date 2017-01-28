using System;
using NUnit.Framework;
using MathParser;

namespace ExpressionTest
{
	[TestFixture]
	public class TestVariables
	{
		[Test]
		public void TestAssignment()
		{
			SymbolManager symbolManager = new SymbolManager();

			MathParser.MathParser parser = new MathParser.MathParser();
			Expression expression = parser.Parse("a = 3");
			expression.ExecuteAssignments(symbolManager);

			double a = parser.Parse("a").Evaluate(symbolManager).ToDouble();
			Assert.AreEqual(3, a);
		}

		[Test]
		public void TestTernary()
		{
			"1<2 ? 5 : 3".ShouldEvaluateTo(5);
			"2<1 ? 5 : 1<2 ? 22 : 20".ShouldEvaluateTo(22);
		}
	}
}
