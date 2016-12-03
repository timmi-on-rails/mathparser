using NUnit.Framework;
using System;
using System.Linq;
using shunting_yard;
using System.Collections.Generic;

namespace ExpressionTest
{
	[TestFixture()]
	public class TokenizerTest
	{
		private void AssertTokensMatch(string expression, params Token[] expectedTokens)
		{
			IEnumerable<Token> actualTokens = Tokenizer.GetTokens(expression);

			Assert.IsTrue(actualTokens.SequenceEqual(expectedTokens));
		}

		private Token Token(TokenType tokenType, string value)
		{
			return new Token(tokenType, value);
		}

		[Test()]
		public void TestCase()
		{
			AssertTokensMatch("x+y", 
				Token(TokenType.Ident, "x"),
				Token(TokenType.Plus, "+"),
				Token(TokenType.Ident, "y")
			);
		}
	}
}

