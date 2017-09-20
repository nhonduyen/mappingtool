using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MappingTool
{
    public class FileHandler
    {
        public string ReadFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        public void WriteFile(string content, string path)
        {
            File.WriteAllText(path, content);
        }
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
        public bool FileExist(string path)
        {
            return File.Exists(path);
        }
    }
}
