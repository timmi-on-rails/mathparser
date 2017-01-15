using System;
using System.Collections.Generic;

namespace MathParser
{
	class TokenStream : IDisposable
	{
		readonly IEnumerator<Token> _tokenEnumerator;
		bool _isEnumeratorAhead;

		public TokenStream(IEnumerable<Token> tokens)
		{
			_tokenEnumerator = tokens.GetEnumerator();
		}

		public Token Consume()
		{
			if (_isEnumeratorAhead)
			{
				_isEnumeratorAhead = false;
			}
			else
			{
				_tokenEnumerator.MoveNext();
			}

			return _tokenEnumerator.Current;
		}

		public Token Consume(TokenType expectedTokenType)
		{
			Token nextToken = Peek();

			if (nextToken.TokenType != expectedTokenType)
			{
				throw new ExpectedTokenException(expectedTokenType, nextToken.Position);
			}

			return Consume();
		}

		public Token Peek()
		{
			if (!_isEnumeratorAhead)
			{
				_tokenEnumerator.MoveNext();
				_isEnumeratorAhead = true;
			}

			return _tokenEnumerator.Current;
		}

		public bool Match(TokenType expectedTokenType)
		{
			Token nextToken = Peek();
			bool isTokenTypeEqual = (nextToken.TokenType == expectedTokenType);

			if (isTokenTypeEqual)
			{
				Consume();
			}

			return isTokenTypeEqual;
		}

		public void Dispose()
		{
			_tokenEnumerator.Dispose();
		}
	}
}
