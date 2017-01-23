using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	class Tokenizer
	{
		static readonly Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>
		{
			{ "true", TokenType.True },
			{ "false", TokenType.False }
		};

		readonly StringBuilder _tokenContentBuilder;
		readonly IEnumerator<char> _charsEnumerator;
		int _index = -1;
		bool _reachedEndOfFile;

		Tokenizer(IEnumerator<char> charsEnumerator)
		{
			_charsEnumerator = charsEnumerator;
			_tokenContentBuilder = new StringBuilder();
		}

		public static IEnumerable<Token> Tokenize(IEnumerable<char> chars)
		{
			return new TokenEnumerable(chars);
		}

		class TokenEnumerable : IEnumerable<Token>
		{
			readonly IEnumerable<char> _chars;

			public TokenEnumerable(IEnumerable<char> chars)
			{
				_chars = chars;
			}

			public IEnumerator<Token> GetEnumerator()
			{
				using (IEnumerator<char> charEnumerator = _chars.GetEnumerator())
				{
					Tokenizer tokenizer = new Tokenizer(charEnumerator);

					foreach (Token token in tokenizer.Scan())
					{
						yield return token;
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		void Advance()
		{
			_index++;
			if (!_charsEnumerator.MoveNext())
			{
				_reachedEndOfFile = true;
			}
		}

		char? Peek()
		{
			if (_reachedEndOfFile)
			{
				return null;
			}
			else
			{
				return _charsEnumerator.Current;
			}
		}

		void Consume()
		{
			if (Peek().HasValue)
			{
				_tokenContentBuilder.Append(Peek().Value);
			}
			Advance();
		}

		Token CreateToken(TokenType tokenType)
		{
			string content = _tokenContentBuilder.ToString();
			_tokenContentBuilder.Clear();
			int startPosition = _index - content.Length;
			return new Token(tokenType, content, startPosition);
		}

		bool IsDigit()
		{
			return Peek().HasValue && char.IsDigit(Peek().Value);
		}

		bool IsEOF()
		{
			return _reachedEndOfFile;
		}

		bool IsIdentifier()
		{
			return IsLetterOrDigit() || Peek() == '_';
		}

		bool IsLetter()
		{
			return Peek().HasValue && char.IsLetter(Peek().Value);
		}

		bool IsLetterOrDigit()
		{
			return Peek().HasValue && char.IsLetterOrDigit(Peek().Value);
		}

		bool IsPunctuation()
		{
			return Peek().HasValue && "<>()!^*+-=/%,?:".Contains(Peek().Value);
		}

		bool IsWhiteSpace()
		{
			return ((Peek().HasValue && char.IsWhiteSpace(Peek().Value)));
		}

		IEnumerable<Token> Scan()
		{
			Advance();

			while (!IsEOF())
			{
				if (IsWhiteSpace())
				{
					yield return ScanWhiteSpace();
				} 
				else if (IsDigit())
				{
					yield return ScanInteger();
				} else if (Peek() == '.')
				{
					yield return ScanFloatingPointNumber();
				}
				else if (IsLetter() || Peek() == '_')
				{
					yield return ScanIdentifier();
				}
				else if (IsPunctuation())
				{
					yield return ScanPunctuation();
				}
				else
				{
					yield return ScanWord();
				}
			}

			yield return CreateToken(TokenType.EndOfFile);
		}

		Token ScanIdentifier()
		{
			while (IsIdentifier())
			{
				Consume();
			}

			if (!IsWhiteSpace() && !IsPunctuation() && !IsEOF())
			{
				return ScanWord();
			}

			TokenType keywordTokenType;
			if (_keywords.TryGetValue(_tokenContentBuilder.ToString(), out keywordTokenType))
			{
				return CreateToken(keywordTokenType);
			}

			return CreateToken(TokenType.Identifier);
		}

		Token ScanInteger()
		{
			while (IsDigit())
			{
				Consume();
			}

			if (Peek() == '.' || Peek() == 'e')
			{
				return ScanFloatingPointNumber();
			}

			return CreateToken(TokenType.Integer);
		}

		Token ScanFloatingPointNumber()
		{
			while (IsDigit())
			{
				Consume();
			}

			if (Peek() == '.')
			{
				Consume();

				bool anyDigits = false;

				while (IsDigit())
				{
					Consume();
					anyDigits = true;
				}

				if (!anyDigits)
				{
					return ScanWord();
				}
			}

			if (Peek() == 'e')
			{
				Consume();

				if (Peek() == '+' || Peek() == '-')
				{
					Consume();
				}

				while (IsDigit())
				{
					Consume();
				}
			}

			return CreateToken(TokenType.FloatingPointNumber);
		}

		Token ScanPunctuation()
		{
			switch (Peek())
			{
				case ':':
					Consume();
					return CreateToken(TokenType.Colon);
				case '(':
					Consume();
					return CreateToken(TokenType.LeftParenthesis);
				case ')':
					Consume();
					return CreateToken(TokenType.RightParenthesis);
				case '>':
					Consume();
					if (Peek() == '=')
					{
						Consume();
						return CreateToken(TokenType.GreaterOrEqual);
					}
					return CreateToken(TokenType.Greater);
				case '<':
					Consume();
					if (Peek() == '=')
					{
						Consume();
						return CreateToken(TokenType.LessOrEqual);
					}
					return CreateToken(TokenType.Less);
				case '+':
					Consume();
					return CreateToken(TokenType.Plus);
				case '-':
					Consume();
					return CreateToken(TokenType.Minus);
				case '=':
					Consume();
					if (Peek() == '=')
					{
						Consume();
						return CreateToken(TokenType.Equal);
					}
					return CreateToken(TokenType.Assignment);
				case '!':
					Consume();
					if (Peek() == '=')
					{
						Consume();
						return CreateToken(TokenType.NotEqual);
					}
					return CreateToken(TokenType.Exclamation);
				case '*':
					Consume();
					return CreateToken(TokenType.Star);
				case '/':
					Consume();
					return CreateToken(TokenType.Slash);
				case '%':
					Consume();
					return CreateToken(TokenType.Percent);
				case ',':
					Consume();
					return CreateToken(TokenType.Comma);
				case '^':
					Consume();
					return CreateToken(TokenType.Pow);
				case '?':
					Consume();
					return CreateToken(TokenType.QuestionMark);
				default:
					return ScanWord();
			}
		}

		Token ScanWhiteSpace()
		{
			while (IsWhiteSpace())
			{
				Consume();
			}

			return CreateToken(TokenType.WhiteSpace);
		}

		Token ScanWord()
		{
			while (!IsWhiteSpace() && !IsEOF() && !IsPunctuation())
			{
				Consume();
			}

			return CreateToken(TokenType.Unknown);
		}
	}
}
