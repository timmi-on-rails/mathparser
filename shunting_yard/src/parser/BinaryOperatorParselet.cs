using System;

namespace shunting_yard
{
	class BinaryOperatorParselet : IInfixParselet
	{
		private readonly int _precedence;

		LeftOrRight _leftOrRight;

		private readonly BinaryExpressionType _binaryExpressionType;

		public int Precedence
		{
			get
			{
				return _precedence;
			}
		}

		public BinaryOperatorParselet(BinaryExpressionType binaryExpressionType, int precedence, LeftOrRight leftOrRight)
		{
			_binaryExpressionType = binaryExpressionType;
			_precedence = precedence;
			_leftOrRight = leftOrRight;
		}

		public IExpression parse(MathParser parser, IExpression left, Token token)
		{
			IExpression right = parser.parseExpression(_precedence - (_leftOrRight == LeftOrRight.Right ? 0 : 0));
			return new BinaryExpression(_binaryExpressionType, left, right);
		}
	}
}
