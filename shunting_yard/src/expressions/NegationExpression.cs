using System;

namespace shunting_yard
{
	class NegationExpression : UnaryExpression
	{
		public NegationExpression(IExpression operand) : base(operand)
		{
		}

		public override double Evaluate()
		{
			return -1 * Operand.Evaluate();
		}

		public override string ToString()
		{
			return String.Format("(-{0})", Operand.ToString());
		}
	}
}
