using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMTipping.Model;

namespace VMTippingClient
{
    public class FileReader
    {
        public async Task<IList<string>> ReadPredictionFiles()
        {
            // Read all files in folder and parse to resultset
            var files = Directory.GetFiles(@"C:\Users\R\Documents\GitHub\VMTipping\PredictionSheets");
            var userFiles = new List<string>();
            var users = new List<User>();
            foreach (var file in files)
            {
                if (file.Contains("~")) continue;
                userFiles.Add(file);
                
            }

            return userFiles;
        }
    }
}
