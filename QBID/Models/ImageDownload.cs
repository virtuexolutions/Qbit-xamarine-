using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
   public class ImageDownload : BindableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public Command CommandDownLoadFile { get; set; }
        public Command CommandOpenFile { get; set; }
    }
}
