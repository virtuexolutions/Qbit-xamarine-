using System.Net.Http;

namespace QBid.DependencyServices
{
    
    public interface IMyOwnNetService
    {
        HttpClientHandler GetHttpClientHandler();
        string GetDownloadPath();
    }
}
