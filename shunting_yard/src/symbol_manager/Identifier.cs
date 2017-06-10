namespace MathParser
{
	public class Identifier
	{
		readonly string _name;

		Identifier(string name)
		{
			_name = name;
			// TODO validation
		}

		public static implicit operator Identifier(string name)
		{
			return new Identifier(name);
		}

		public static implicit operator string(Identifier identifier)
		{
			return identifier._name;
		}

		public override string ToString()
		{
			return _name;
		}

		public override bool Equals(object obj)
		{
			Identifier other = (obj as Identifier);

			if ((object)other == null)
			{
				return false;
			}

			return string.Equals(_name, other._name);
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		public static bool operator ==(Identifier identifier1, Identifier identifier2)
		{
			if (object.ReferenceEquals(identifier1, identifier2))
			{
				return true;
			}

			if ((object)identifier1 == null || (object)identifier2 == null)
			{
				return false;
			}

			return string.Equals(identifier1._name, identifier2._name);
		}

		public static bool operator !=(Identifier identifier1, Identifier identifier2)
		{
			return !(identifier1 == identifier2);
		}
	}
}
