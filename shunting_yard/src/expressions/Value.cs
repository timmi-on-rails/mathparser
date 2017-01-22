using System;

namespace MathParser
{
	public delegate Value Function(params Value[] arguments);

	public class Value
	{
		enum ValueType
		{
			Integer,
			FloatingPointNumber,
			Boolean,
			Function
		}

		readonly object _value;
		readonly ValueType _valueType;

		public bool IsInteger
		{
			get { return _valueType == ValueType.Integer; }
		}

		public bool IsFloatingPointNumber
		{
			get { return _valueType == ValueType.FloatingPointNumber; }
		}

		public bool IsBoolean
		{
			get { return _valueType == ValueType.Boolean; }
		}

		public bool IsFunction
		{
			get { return _valueType == ValueType.Function; }
		}

		Value(ValueType valueType, object value)
		{
			_valueType = valueType;
			_value = value;
		}

		public static Value Integer(long l)
		{
			return new Value(ValueType.Integer, l);
		}

		public static Value FloatingPointNumber(double d)
		{
			return new Value(ValueType.FloatingPointNumber, d);
		}

		public static Value Boolean(bool b)
		{
			return new Value(ValueType.Boolean, b);
		}

		public static Value Function(Function function)
		{
			return new Value(ValueType.Function, function);
		}

		public long ToInt64()
		{
			return Convert.ToInt64(_value);
		}

		public double ToDouble()
		{
			return Convert.ToDouble(_value);
		}

		public bool ToBoolean()
		{
			return Convert.ToBoolean(_value);
		}

		public Function ToFunction()
		{
			return (Function)_value;
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}
}
