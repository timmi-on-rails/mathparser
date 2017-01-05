using System;

namespace MathParser
{
	class BadAssignmentException : ParserException
	{
		internal BadAssignmentException() { }
		internal BadAssignmentException(string message) : base(message) { }
		internal BadAssignmentException(string message, Exception innerException) : base(message, innerException) { }
	}
}
