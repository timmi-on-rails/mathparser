using System;
using System.Collections.Generic;

namespace shunting_yard
{
	struct Operator
	{
		public TokenType TokenType { get; }

		public OperatorType OperatorType { get; }

		public Associativity Associativity { get; }

		public int Priority { get; }

		public Action<MathParser, Stack<IExpression>> Apply { get; }

		public int ArgumentCount { get; set; }

		public Operator(TokenType tokenType, OperatorType operatorType, Associativity associativity, int priority, Action<MathParser, Stack<IExpression>> apply)
		{
			TokenType = tokenType;
			OperatorType = operatorType;
			Associativity = associativity;
			Priority = priority;
			Apply = apply;
			ArgumentCount = 0;
		}
	}
}
