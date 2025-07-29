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
    internal class FileIOService
    {
        public readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }
       
        public BindingList<LibModel> LoadData()
        {
            var fileExist = File.Exists(PATH);
            
            if (!fileExist) 
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<LibModel>();
            }
            
            var fileText = File.ReadAllText(PATH);

            if (string.IsNullOrEmpty(fileText)) 
                return new BindingList<LibModel>();

            try
            {
                var res = JsonConvert.DeserializeObject<BindingList<LibModel>>(fileText);
                return res ?? new BindingList<LibModel>();
            }
            catch
            {
                return new BindingList<LibModel>();
            }
        }

        public void SaveData(BindingList<LibModel> libraryBooks) 
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
