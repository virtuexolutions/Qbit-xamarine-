using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QBid.DependencyServices
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
        byte[] GetJpgFromHEIC(string path);
    }
}
