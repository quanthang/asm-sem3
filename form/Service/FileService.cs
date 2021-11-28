using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace form.Service
{
    public class FileService
    {
        private static string CloudName = "cuong0508";
        private static string CloudApiKey = "999991294818679";
        private static string CloudApiSecret = "5e38idmUyqN8gRNT7EWHVnQ5Haw";

        static CloudinaryDotNet.Account account;
        static CloudinaryDotNet.Cloudinary cloud;

        public FileService()
        {
            if (account == null)
            {
                account = new CloudinaryDotNet.Account
                {
                    Cloud = CloudName,
                    ApiKey = CloudApiKey,
                    ApiSecret = CloudApiSecret
                };
            }

            if (cloud ==null)
            {
                cloud = new CloudinaryDotNet.Cloudinary(account);
                cloud.Api.Secure = true;
            }
        }

        public async Task<string> UploadFile(Windows.Storage.StorageFile file)
        {
            if(file != null)
            {
                Debug.WriteLine(file.Path);
                CloudinaryDotNet.Actions.RawUploadParams imageUploadParams = new CloudinaryDotNet.Actions.RawUploadParams
                {
                    File = new CloudinaryDotNet.FileDescription(@file.Name, await file.OpenStreamForReadAsync())
                };
                CloudinaryDotNet.Actions.RawUploadResult result = cloud.Upload(imageUploadParams);
                return result.SecureUrl.OriginalString;
            }
            return null;
        }
    }
}
