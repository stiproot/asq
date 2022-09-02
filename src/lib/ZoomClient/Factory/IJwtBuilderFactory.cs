using ZoomClient.Builder;

namespace ZoomClient.Factory
{
    public interface IJwtBuilderFactory
    {
        IJwtBuilder Create();
    }
}