namespace Mvc
{
public interface IValueProvider
{
    bool TryGetValues(string name, out string[] values);
    bool ContainsPrefix(string prefix);
}
}
