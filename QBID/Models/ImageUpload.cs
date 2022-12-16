using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class ImageUpload : BindableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Stream FileName { get; set; }
    }
}
