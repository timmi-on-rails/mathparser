using NUnit.Framework;
using System;
using shunting_yard;

namespace ExpressionTest
{

	// Test tokenizer
	// Test function Manager


	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestAddition()
		{
			Assert.AreEqual(4, "2+2".Evaluate());
		}

		[Test()]
		public void TestPunktVorStrich()
		{
			Assert.AreEqual(16, "8+4*2".Evaluate());
		}

		[Test()]
		public void TestDivision()
		{
			Assert.AreEqual(0.5, "8/16".Evaluate());
		}

		[Test()]
		public void TestPrecedence()
		{
			Assert.AreEqual(4, "10-3-3".Evaluate());
		}

		[Test()]
		public void TestPrecedence2()
		{
			Assert.AreEqual(1, "20/4/5".Evaluate());
		}

		[Test()]
		public void TestParens()
		{
			Assert.AreEqual(14, "(4+3)*2".Evaluate());
		}

		[Test()]
		public void TestPow()
		{
			Assert.AreEqual(8, "2^3".Evaluate());
		}

		[Test()]
		public void TestNegation()
		{
			Assert.AreEqual(-1, "-1".Evaluate());
		}

		[Test()]
		public void TestDoublePow()
		{
			Assert.AreEqual(81, "3^2^2".Evaluate());
		}

		[Test()]
		public void TestMax()
		{
			Assert.AreEqual(6, "2+Max(2 ,4 ,-20)".Evaluate());
		}
	}
}
