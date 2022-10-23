using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Services
{
    interface IFileService
    {
        public void Save(string filePath, string text);
        public string Read(string filepath);
    }
    internal class FileService : IFileService
    {
        

        public FileService()
        {
            
        }
        //Sparar till .json fil
        public void Save(string filePath, string text)
        {
            try
            {
                using var sw = new StreamWriter(filePath);
                sw.WriteLine(text);
            }
            catch 
            {
                
            }


        }
        //Läser innehållet av .json fil
        public string Read(string filePath)
        {
            try
            {
                using var sr = new StreamReader(filePath);
                return sr.ReadToEnd();
            }
            catch { }

            return "[]";
        }
    }
}
