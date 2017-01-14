namespace MathParser
{
	public interface IFunctionProvider
	{
		bool IsDefined(string functionName, int argumentCount);
		object Call(string functionName, object[] arguments);
	}
}
