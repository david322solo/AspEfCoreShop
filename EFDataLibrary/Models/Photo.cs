using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataLibrary.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string MimeType { get; set; }
        public byte[] Image { get; set; }
        public List<ProductPhoto> ProductPhotos { get; set; }
        public Photo()
        {
            ProductPhotos = new List<ProductPhoto>();
        }
    }
}
