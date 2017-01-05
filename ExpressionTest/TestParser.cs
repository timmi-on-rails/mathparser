using NUnit.Framework;

namespace ExpressionTest
{
	[TestFixture]
	public class TestParser
	{
		[Test]
		public void TestPointBeforeLine()
		{
			"1 + 2 * 3".ShouldParseAs("(1+(2*3))");
			"5 * 3 + 10".ShouldParseAs("((5*3)+10)");
			"1 + 4 * 10 + 3".ShouldParseAs("((1+(4*10))+3)");
			"1 * 2 + 3 * 4".ShouldParseAs("((1*2)+(3*4))");
		}

		[Test]
		public void PrefixPrecedence()
		{
			"-2^3".ShouldParseAs("(-(2^3))");
			"(-2)^3".ShouldParseAs("((-2)^3)");
			"16 + -5^4".ShouldParseAs("(16+(-(5^4)))");
			"16 - 5^4".ShouldParseAs("(16-(5^4))");
		}
	}
}
