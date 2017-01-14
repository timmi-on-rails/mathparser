namespace MathParser
{
	public interface IVariableProvider
	{
		bool IsSet(string variableName);
		object Get(string variableName);
	}
}
