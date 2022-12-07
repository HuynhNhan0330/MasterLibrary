using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MasterLibrary.Utils
{
    public class CloudinaryService
    {
        private static CloudinaryService _ins;
        public static CloudinaryService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CloudinaryService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        private Account account;
        private Cloudinary cloudinary;
        private CloudinaryService()
        {
            account = new Account("dclzpc4gd", "673248188376562", "YuE5o0-X2csdTsCaUQXt9BCYNMs");
            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImage(string filePath)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    Folder = "MasterLibrary"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return uploadResult.SecureUrl.AbsoluteUri;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BitmapImage> LoadImageFromURL(string imageURL)
        {
            if (string.IsNullOrEmpty(imageURL))
            {
                return null;
            }

            System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
            System.Net.HttpWebResponse response;
            try
            {
                response = (await request.GetResponseAsync()) as System.Net.HttpWebResponse;
            }
            catch (System.Net.WebException)
            {
                return null;
            }

            System.IO.Stream responseStream = response.GetResponseStream();

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = responseStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            return bitmap;
        }
    }
}
