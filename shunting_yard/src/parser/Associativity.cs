using System;

namespace MathParser
{
	enum Associativity
	{
		Left,
		Right
	}

	static class AssociativityExtensions
	{
		/// <summary>
		/// Parsing operators with same precedence and right associativity
		/// is done by parsing the right hand side expression with a lower precedence.
		/// </summary>
		/// <returns>The precedence increment.</returns>
		public static int ToPrecedenceIncrement(this Associativity associativity)
		{
			switch (associativity)
			{
				case Associativity.Left:
					return 0;
				case Associativity.Right:
					return -1;
				default:
					throw new ArgumentException("unhandled associtivity");
			}
		}
	}
}
