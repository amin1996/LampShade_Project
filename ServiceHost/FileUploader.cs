using _0_Framework.Application;
using _0_FrameWork.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file,string path)
        {
            if (file == null)
                return "";

            var Directorypath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";

            if(!Directory.Exists(Directorypath))
                Directory.CreateDirectory(Directorypath);

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";

            var filePath = $"{Directorypath}//{fileName}";

            using var output = System.IO.File.Create(filePath);
            file.CopyTo(output);

            return $"{path}/{fileName}";
        }
    }
}
