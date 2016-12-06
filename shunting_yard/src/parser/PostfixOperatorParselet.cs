using System;
namespace shunting_yard
{
	class PostfixOperatorParselet : IInfixParselet
	{
		public PostfixOperatorParselet(int precedence)
		{
			mPrecedence = precedence;
		}

		public IExpression parse(MathParser parser, IExpression left, Token token)
		{
			return new PostfixExpression();
		}

		public int getPrecedence()
		{
			return mPrecedence;
		}



		private int mPrecedence;

		public int Precedence
		{
			get
			{
				return mPrecedence;
			}
		}
	}
}
