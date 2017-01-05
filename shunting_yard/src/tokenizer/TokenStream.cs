using System.Collections.Generic;

namespace MathParser
{
	class TokenStream
	{
		readonly Queue<Token> _tokenQueue;

		public TokenStream(string input)
		{
			_tokenQueue = new Queue<Token>(Tokenizer.GetTokens(input));
		}

		public Token Consume()
		{
			return _tokenQueue.Dequeue();
		}

		public Token Consume(TokenType expected)
		{
			Token token = Peek();
			if (token.TokenType != expected)
			{
				throw new ExpectedTokenException(expected, token.Position);
			}

			return Consume();
		}

		public Token Peek()
		{
			return _tokenQueue.Peek();
		}

		public bool Match(TokenType expected)
		{
			Token token = Peek();
			if (token.TokenType != expected)
			{
				return false;
			}

			Consume();
			return true;
		}
	}
}
