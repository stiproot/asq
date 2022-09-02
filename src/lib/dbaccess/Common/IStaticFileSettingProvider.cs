using System.Threading.Tasks;

namespace dbaccess.Common
{
    public interface IStaticFileSettingProvider
    {
        string GetImgBasePath();
        string GetVideoBasePath();
        Task<string> GetImgBasePathAsync();
        string GetStaticImgServerBaseUrl();
        string GetStaticVideoServerBaseUrl();
    }
}