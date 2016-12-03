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
	}
}
