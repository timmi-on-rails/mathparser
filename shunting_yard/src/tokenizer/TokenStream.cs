using System;
using System.Collections.Generic;

namespace MathParser
{
	class TokenStream : IDisposable
	{
		readonly IEnumerator<Token> _tokenEnumerator;
		bool alreadyPeeked;

		public TokenStream(string input)
		{
			_tokenEnumerator = Tokenizer.GetTokens(input).GetEnumerator();
		}
		/*
		public static IEnumerable<char> ReadCharacters(string filename)
		{
			using (var reader = File.OpenText(filename))
			{
				int readResult;
				while ((readResult = reader.Read()) != -1)
				{
					yield return (char)readResult;
				}
			}
		}
		*/

		public Token Consume()
		{
			if (alreadyPeeked)
			{
				alreadyPeeked = false;
				return _tokenEnumerator.Current;
			}
			else
			{
				_tokenEnumerator.MoveNext();
				return _tokenEnumerator.Current;
			}
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
			if (alreadyPeeked)
			{
				return _tokenEnumerator.Current;
			}
			else
			{
				_tokenEnumerator.MoveNext();
				alreadyPeeked = true;
				return _tokenEnumerator.Current;
			}
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

		public void Dispose()
		{
			_tokenEnumerator.Dispose();
		}
	}
}
