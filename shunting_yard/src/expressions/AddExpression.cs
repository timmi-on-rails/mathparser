using System;

namespace shunting_yard
{
	class AddExpression : BinaryExpression
	{
		public AddExpression(IExpression operand1, IExpression operand2) : base(operand1, operand2)
		{
		}

		public override double Evaluate()
		{
			return Operand1.Evaluate() + Operand2.Evaluate();
		}

		public override string ToString()
		{
			return String.Format("({0} + {1})", Operand1.ToString(), Operand2.ToString());
		}
	}
}
