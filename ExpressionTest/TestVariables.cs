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
			MathParser.MathParser parser = new MathParser.MathParser();
			ExpressionTree tree = parser.Parse("a = 3");
			tree.Assign();

			double a = parser.Parse("a").Evaluate();
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
