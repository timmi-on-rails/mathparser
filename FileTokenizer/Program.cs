using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MathParser;

namespace FileTokenizer
{
	class MainClass
	{
		static IEnumerable<char> GetChars (string fileName)
		{
			using (var file = File.OpenText (fileName)) {
				while (true) {
					int nextChar = file.Read ();

					if (nextChar == -1) {
						yield break;
					}

					yield return (char)nextChar;
				}
			}
		}

		public static void Main (string [] args)
		{
			int totalUnknown = 0;

			foreach (string fileName in Directory.EnumerateFiles (args [0], "*.cs", SearchOption.AllDirectories)) {
				IEnumerable<Token> tokens = Tokenizer.Tokenize (GetChars (fileName));

				IEnumerable<Token> uTokens = tokens.Where (token => token.TokenType == TokenType.Unknown).ToList ();
				if (uTokens.Count () > 0) {
					Console.WriteLine ("Found {0} unknown tokens in file {1}: {2},...", uTokens.Count (), fileName,
									  string.Join (", ", uTokens.Take (5).Select (token => token.Content)));
				}
				totalUnknown += uTokens.Count ();
			}

			Console.WriteLine ("Total unknown tokens: {0}", totalUnknown);
		}
	}
}
