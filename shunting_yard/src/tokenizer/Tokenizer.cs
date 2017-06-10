using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser
{
	public class Tokenizer
	{
		static readonly Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>
		{
			{ "true", TokenType.True },
			{ "false", TokenType.False }
		};

		readonly StringBuilder _tokenContentBuilder;
		readonly CharStream charStream;

		Tokenizer (CharStream charStream)
		{
			this.charStream = charStream;
			_tokenContentBuilder = new StringBuilder ();
		}

		public static IEnumerable<Token> Tokenize (IEnumerable<char> chars)
		{
			using (CharStream charStream = new CharStream (chars)) {
				Tokenizer tokenizer = new Tokenizer (charStream);

				foreach (Token token in tokenizer.Scan ()) {
					yield return token;
				}
			}
		}

		char? Peek ()
		{
			return charStream.Peek ();
		}

		void Consume ()
		{
			if (Peek ().HasValue) {
				_tokenContentBuilder.Append (Peek ().Value);
			}
			charStream.Advance ();
		}

		Token CreateToken (TokenType tokenType)
		{
			string content = _tokenContentBuilder.ToString ();
			_tokenContentBuilder.Clear ();
			int startPosition = charStream.Index - content.Length;
			return new Token (tokenType, content, startPosition);
		}

		bool IsDigit ()
		{
			return Peek ().HasValue && char.IsDigit (Peek ().Value);
		}

		bool IsEOF ()
		{
			return !charStream.Peek ().HasValue;
		}

		bool IsIdentifier ()
		{
			return IsLetterOrDigit () || Peek () == '_';
		}

		bool IsLetter ()
		{
			return Peek ().HasValue && char.IsLetter (Peek ().Value);
		}

		bool IsLetterOrDigit ()
		{
			return Peek ().HasValue && char.IsLetterOrDigit (Peek ().Value);
		}

		bool IsPunctuation ()
		{
			return Peek ().HasValue && "<>()!^*+-=/%,?:;.{}[]".Contains (Peek ().Value);
		}

		bool IsWhiteSpace ()
		{
			return ((Peek ().HasValue && char.IsWhiteSpace (Peek ().Value)));
		}

		bool IsNewLine ()
		{
			return Peek ().HasValue && (Peek ().Value == '\r' || Peek ().Value == '\n');
		}

		IEnumerable<Token> Scan ()
		{
			charStream.Advance ();

			while (!IsEOF ()) {
				if (IsNewLine ()) {
					yield return ScanNewLine ();
				} else if (IsWhiteSpace ()) {
					yield return ScanWhiteSpace ();
				} else if (IsDigit ()) {
					yield return ScanInteger ();
				} else if (Peek () == '"') {
					yield return ScanString ();
				} else if (Peek () == '.') {
					yield return ScanFloatingPointNumber ();
				} else if (IsLetter () || Peek () == '_') {
					yield return ScanIdentifier ();
				} else if (IsPunctuation ()) {
					yield return ScanPunctuation ();
				} else {
					yield return ScanWord ();
				}
			}

			yield return CreateToken (TokenType.EndOfFile);
		}

		Token ScanIdentifier ()
		{
			while (IsIdentifier ()) {
				Consume ();
			}

			if (!IsWhiteSpace () && !IsPunctuation () && !IsEOF ()) {
				return ScanWord ();
			}

			TokenType keywordTokenType;
			if (_keywords.TryGetValue (_tokenContentBuilder.ToString (), out keywordTokenType)) {
				return CreateToken (keywordTokenType);
			}

			return CreateToken (TokenType.Identifier);
		}

		Token ScanString ()
		{
			Consume ();

			while (Peek ().HasValue && Peek ().Value != '"' && Peek ().Value != '\r' && Peek ().Value != '\n') {
				Consume ();
			}

			if (Peek () == '"') {
				Consume ();
				return CreateToken (TokenType.String);
			}

			return ScanWord ();
		}

		Token ScanInteger ()
		{
			while (IsDigit ()) {
				Consume ();
			}

			if (Peek () == '.' || Peek () == 'e') {
				return ScanFloatingPointNumber ();
			}

			return CreateToken (TokenType.Integer);
		}

		Token ScanFloatingPointNumber ()
		{
			while (IsDigit ()) {
				Consume ();
			}

			if (Peek () == '.') {
				Consume ();

				bool anyDigits = false;

				while (IsDigit ()) {
					Consume ();
					anyDigits = true;
				}

				if (!anyDigits) {
					if (_tokenContentBuilder.Length == 1) {
						return CreateToken (TokenType.Dot);
					}

					return ScanWord ();
				}
			}

			if (Peek () == 'e') {
				Consume ();

				if (Peek () == '+' || Peek () == '-') {
					Consume ();
				}

				while (IsDigit ()) {
					Consume ();
				}
			}

			return CreateToken (TokenType.Decimal);
		}

		Token ScanPunctuation ()
		{
			switch (Peek ()) {
			case ':':
				Consume ();
				return CreateToken (TokenType.Colon);
			case '(':
				Consume ();
				return CreateToken (TokenType.LeftParenthesis);
			case ')':
				Consume ();
				return CreateToken (TokenType.RightParenthesis);
			case '>':
				Consume ();
				if (Peek () == '=') {
					Consume ();
					return CreateToken (TokenType.GreaterOrEqual);
				}
				return CreateToken (TokenType.Greater);
			case '<':
				Consume ();
				if (Peek () == '=') {
					Consume ();
					return CreateToken (TokenType.LessOrEqual);
				}
				if (Peek () == '>') {
					Consume ();
					return CreateToken (TokenType.NotEqual);
				}
				return CreateToken (TokenType.Less);
			case '+':
				Consume ();
				return CreateToken (TokenType.Plus);
			case '-':
				Consume ();
				return CreateToken (TokenType.Minus);
			case '=':
				Consume ();
				if (Peek () == '=') {
					Consume ();
					return CreateToken (TokenType.Equal);
				}
				return CreateToken (TokenType.Assignment);
			case '!':
				Consume ();
				if (Peek () == '=') {
					Consume ();
					return CreateToken (TokenType.NotEqual);
				}
				return CreateToken (TokenType.Exclamation);
			case '*':
				Consume ();
				return CreateToken (TokenType.Star);
			case '/':
				Consume ();
				return CreateToken (TokenType.Slash);
			case '%':
				Consume ();
				return CreateToken (TokenType.Percent);
			case ',':
				Consume ();
				return CreateToken (TokenType.Comma);
			case '^':
				Consume ();
				return CreateToken (TokenType.Pow);
			case '?':
				Consume ();
				return CreateToken (TokenType.QuestionMark);
			case ';':
				Consume ();
				return CreateToken (TokenType.Semicolon);
			case '.':
				Consume ();
				return CreateToken (TokenType.Dot);
			case '{':
				Consume ();
				return CreateToken (TokenType.CurlyLeft);
			case '}':
				Consume ();
				return CreateToken (TokenType.CurlyRight);
			case '[':
				Consume ();
				return CreateToken (TokenType.BracketLeft);
			case ']':
				Consume ();
				return CreateToken (TokenType.BracketRight);
			default:
				return ScanWord ();
			}
		}

		Token ScanWhiteSpace ()
		{
			while (IsWhiteSpace ()) {
				Consume ();
			}

			return CreateToken (TokenType.WhiteSpace);
		}

		Token ScanNewLine ()
		{
			if (Peek () == '\n') {
				Consume ();
				return CreateToken (TokenType.NewLine);
			} else if (Peek () == '\r') {
				// TODO do lookahead more than one character to not needing to consume it
				Consume ();
				if (Peek () == '\n') {
					Consume ();
					return CreateToken (TokenType.NewLine);
				}
			}

			return ScanWord ();
		}

		Token ScanWord ()
		{
			while (!IsWhiteSpace () && !IsEOF () && !IsPunctuation ()) {
				Consume ();
			}

			return CreateToken (TokenType.Unknown);
		}
	}
}
