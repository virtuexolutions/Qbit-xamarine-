using QBid.DependencyServices;
using QBid.Droid.DependencyServices;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyOwnNetService))]
namespace QBid.Droid.DependencyServices
{
    class MyOwnNetService : IMyOwnNetService
    {
        public HttpClientHandler GetHttpClientHandler()
        {
            return new Xamarin.Android.Net.AndroidClientHandler();
        }
        public string GetDownloadPath()
        {
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);            
        }
    }
}