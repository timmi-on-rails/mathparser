using System;

namespace MathParser
{
	class ParserException : Exception
	{
		internal ParserException() { }
		internal ParserException(string message) : base(message) { }
		internal ParserException(string message, Exception innerException) : base(message, innerException) { }
	}
}
