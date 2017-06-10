using System;
using MathParser;
using NUnit.Framework;

namespace ExpressionTest
{
	[TestFixture]
	public class TestCharStream
	{
		[Test]
		public void TestBasicFunctionality ()
		{
			using (CharStream charStream = new CharStream (new [] { 'a', 'b', 'c' })) {
				Assert.IsTrue (charStream.IsAheadOfStart);
				Assert.IsFalse (charStream.IsAfterEnd);
				Assert.AreEqual (-1, charStream.Index);
				Assert.AreEqual (null, charStream.Peek ());

				charStream.Advance ();

				Assert.IsFalse (charStream.IsAheadOfStart);
				Assert.IsFalse (charStream.IsAfterEnd);
				Assert.AreEqual (0, charStream.Index);
				Assert.AreEqual ('a', charStream.Peek ());

				charStream.Advance ();

				Assert.IsFalse (charStream.IsAheadOfStart);
				Assert.IsFalse (charStream.IsAfterEnd);
				Assert.AreEqual (1, charStream.Index);
				Assert.AreEqual ('b', charStream.Peek ());

				charStream.Advance ();

				Assert.IsFalse (charStream.IsAheadOfStart);
				Assert.IsFalse (charStream.IsAfterEnd);
				Assert.AreEqual (2, charStream.Index);
				Assert.AreEqual ('c', charStream.Peek ());

				charStream.Advance ();

				Assert.IsFalse (charStream.IsAheadOfStart);
				Assert.IsTrue (charStream.IsAfterEnd);
				Assert.AreEqual (3, charStream.Index);
				Assert.AreEqual (null, charStream.Peek ());

				Assert.Throws<InvalidOperationException> (() => charStream.Advance ());
			}
		}

		[Test]
		public void TestEmptyCharStream ()
		{
			using (CharStream charStream = new CharStream (new char [0])) {
				Assert.IsTrue (charStream.IsAheadOfStart);
				Assert.IsFalse (charStream.IsAfterEnd);
				Assert.AreEqual (-1, charStream.Index);
				Assert.AreEqual (null, charStream.Peek ());

				charStream.Advance ();

				Assert.IsFalse (charStream.IsAheadOfStart);
				Assert.IsTrue (charStream.IsAfterEnd);
				Assert.AreEqual (0, charStream.Index);
				Assert.AreEqual (null, charStream.Peek ());

				Assert.Throws<InvalidOperationException> (() => charStream.Advance ());
			}
		}

		[Test]
		public void TestLookAhead ()
		{
			using (CharStream charStream = new CharStream (new [] { 'a', 'b', 'c' })) {
				Assert.AreEqual ('a', charStream.Peek (1));
				Assert.AreEqual ('b', charStream.Peek (2));
				Assert.AreEqual ('c', charStream.Peek (3));
			}
		}
	}
}
