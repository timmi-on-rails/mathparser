using System;
using NUnit.Framework;
using MathParser;

namespace ExpressionTest
{
	[TestFixture]
	public class TestFunctions
	{
		[Test]
		public void TestFuncAssignment1()
		{
			MathParser.MathParser parser = new MathParser.MathParser();
			ExpressionTree tree = parser.Parse("f(x)=3");
			tree.Assign();

			double a = parser.Parse("f(2)").Evaluate();
			Assert.AreEqual(3, a);
		}

		[Test]
		public void TestFuncAssignment2()
		{
			MathParser.MathParser parser = new MathParser.MathParser();
			ExpressionTree tree = parser.Parse("f(x)=x*x");
			tree.Assign();

			double a = parser.Parse("f(5)").Evaluate();
			Assert.AreEqual(25, a);
		}
	}
}
