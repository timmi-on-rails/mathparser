using System;

namespace shunting_yard
{
	class DivExpression : BinaryExpression
	{
		public DivExpression(IExpression operand1, IExpression operand2) : base(operand1, operand2)
		{
		}

		public override bool CanEvaluate()
		{
			return Operand1.CanEvaluate() && Operand2.CanEvaluate() && Operand2.Evaluate() != 0;
		}

		public override double Evaluate()
		{
			return Operand1.Evaluate() / Operand2.Evaluate();
		}

		public override string ToString()
		{
			return String.Format("({0} / {1})", Operand1.ToString(), Operand2.ToString());
		}
	}
}
