using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Extensions
{
    public static class IFormFileExtensions
    {

        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsLessThan(this IFormFile file, int mb)
        {
            return (file.Length / 1024 / 1024) > mb;
        }

        public async static Task<string> Save(this IFormFile file, string root, string folder)
        {
            string path = Path.Combine(root, "img");

            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fileNameWithFolder = Path.Combine(folder, fileName);


            string resultPath = Path.Combine(path, fileNameWithFolder);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            };

            return fileName;
        }
    }
}