namespace MathParser
{
	class Precedences
	{
		public const int EXPRESSION = 0;
		public const int ASSIGNMENT = 1;
		public const int CONDITIONAL = 2;
		public const int COMPARISON = 3;
		public const int SUM = 4;
		public const int PRODUCT = 5;
		public const int PREFIX = 6;
		public const int EXPONENT = 7;
		public const int POSTFIX = 8;
		public const int CALL = 9;
	}
}
