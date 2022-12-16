using System;
using QBid.DependencyServices;
using QBid.iOS.DependencyServices;
using Xamarin.Forms;
using System.IO;
using System.Net.Http;

[assembly: Dependency(typeof(MyOwnNetService))]
namespace QBid.iOS.DependencyServices
{
    public class MyOwnNetService : IMyOwnNetService
    {
        public MyOwnNetService()
        {

        }
        public HttpClientHandler GetHttpClientHandler()
        {
            return new HttpClientHandler();
        }

        public string GetDownloadPath()
        {          

            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
             return documentsPath;

            
         }      
    }
}
