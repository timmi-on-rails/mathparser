using System;
using System.Collections.Generic;

namespace shunting_yard
{
	public interface IExpression
	{
		IContext Context { get; set; }

		bool CanEvaluate();

		IEnumerable<IExpression> Children { get; }

		double Evaluate();
	}
}

