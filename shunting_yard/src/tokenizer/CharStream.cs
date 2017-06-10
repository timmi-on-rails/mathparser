using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MathParser
{
	public class CharStream : IDisposable
	{
		readonly IEnumerator<char> charEnumerator;
		List<char> buffer;

		public CharStream (IEnumerable<char> characters)
		{
			charEnumerator = characters.GetEnumerator ();
			Index = -1;
			buffer = new List<char> ();
		}

		public int Index { get; private set; }

		public bool IsAheadOfStart { get { return Index < 0; } }

		bool hasReachedEnd;
		public bool IsAfterEnd { get { return hasReachedEnd && buffer.Count == 0; } }

		/// <summary>
		/// Returns the character at the current index. (optionally you can apply an offset to the current index)
		/// If either the index is smaller than the first character's index
		/// or greater than the last character's index, the method returns null.
		/// </summary>
		public char? Peek (int offset = 0)
		{
			if (offset < 0) {
				throw new ArgumentOutOfRangeException (nameof (offset), "must be >= 0");
			}

			int peekPosition = Index + offset;

			if (peekPosition < 0) {
				return null;
			}

			int neededSteps = offset - buffer.Count;

			if (neededSteps >= 0) {
				for (int i = 0; i < neededSteps; i++) {
					if (Index >= 0) {
						buffer.Add (charEnumerator.Current);
					}
					if (!charEnumerator.MoveNext ()) {
						hasReachedEnd = true;
						break;
					}
				}

				if (hasReachedEnd) {
					return null;
				} else {
					return charEnumerator.Current;
				}
			} else {
				return buffer [offset];
			}

			//				Debug.Assert (!(IsAfterEnd && buffer.Count != 0), "if index is after end, the buffer must be empty");
		}

		public void Advance ()
		{
			if (IsAfterEnd) {
				throw new InvalidOperationException ("CharStream has reached end of input.");
			}

			Index++;

			if (buffer.Count > 0) {
				buffer.RemoveAt (0);
			} else if (!charEnumerator.MoveNext ()) {
				hasReachedEnd = true;
			}
		}

		public void Dispose ()
		{
			charEnumerator.Dispose ();
		}
	}
}
