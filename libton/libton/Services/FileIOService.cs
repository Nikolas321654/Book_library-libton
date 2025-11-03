using Libton.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libton.Servises
{
    public class FileIOService
    {
        public readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }
       
        public BindingList<BookModel> LoadData()
        {
            var fileExist = File.Exists(PATH);
            
            if (!fileExist) 
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<BookModel>();
            }
            
            var fileText = File.ReadAllText(PATH);

            if (string.IsNullOrEmpty(fileText)) 
                return new BindingList<BookModel>();

            try
            {
                var res = JsonConvert.DeserializeObject<BindingList<BookModel>>(fileText);
                return res ?? new BindingList<BookModel>();
            }
            catch
            {
                return new BindingList<BookModel>();
            }
        }

        public void SaveData(BindingList<BookModel> libraryBooks) 
        {
            if (libraryBooks == null)
                throw new ArgumentNullException(nameof(libraryBooks));

            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(libraryBooks);
                writer.Write(output);
            } 
        }
    }
}
