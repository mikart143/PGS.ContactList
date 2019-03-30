using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace PGS.ContactList.Services
{
    public class FileService
    {
        private readonly string StorageFolderName = "upload";
        private readonly string StorageFolderPath;
        private readonly IHostingEnvironment _env;
        public FileService(IHostingEnvironment env)
        {
            _env = env;
            StorageFolderPath = Path.Combine(_env.WebRootPath, StorageFolderName);
            if (!Directory.Exists(StorageFolderPath)) Directory.CreateDirectory(StorageFolderPath);

        }

        public bool IsImage(Stream stream)
        {
            try
            {
                using (var image = Image.FromStream(stream)) { }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SaveFile(Stream photoStream, string fileName)
        {
            var filePath = Path.Combine(StorageFolderPath, fileName);
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await photoStream.CopyToAsync(fileStream);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                File.Delete(Path.Combine(StorageFolderPath,fileName));
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public FileStream GetFile(string fileName)
        {
            var filePath = Path.Combine(StorageFolderPath, fileName);

            return new FileStream(filePath, FileMode.Open);

        }

        public string GetFileWebPath(string fileName)
        {
            return "/"+StorageFolderName + "/" + fileName;
        }
    }
}
