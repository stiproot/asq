namespace processes.Engine
{
    public class TypeCaster: ITypeCaster
    {
        public T Cast<T>(object toCast) => (T)toCast;
    }
}