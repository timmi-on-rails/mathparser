using NUnit.Framework;

namespace ExpressionTest
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void TestSum()
		{
			"2.1+3".ShouldEvaluateTo(5.1);
			"2+9".ShouldEvaluateTo(11);
			"102-4+2-1-1".ShouldEvaluateTo(98);
			"1+9".ShouldEvaluateTo(10);
		}

		[Test]
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
		public void TestDoublePowAssociativity()
		{
			Assert.AreEqual(512, "2^3^2".Evaluate());
		}

		//[Test()]
		public void TestMax()
		{
			Assert.AreEqual(6, "2+Max(2 ,4 ,-20)".Evaluate());
		}

		[Test]
		public void TestWhiteSpace()
		{
			"(1+2) + 3".ShouldEvaluateTo(6);
		}

		[Test]
		public void TestTernary()
		{
			"(1<2) ? 200 : 300".ShouldEvaluateTo(200);
		}
	}
}
