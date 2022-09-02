namespace processes.Engine
{
    public interface ITypeCaster
    {
        T Cast<T>(object toCast);
    }
}