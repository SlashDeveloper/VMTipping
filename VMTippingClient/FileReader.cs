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
        public IList<User> ReadPredictionFiles()
        {
            // Read all files in folder and parse to resultset
            var files = Directory.GetFiles(@"C:\Users\R\Documents\GitHub\VMTipping\PredictionSheets");
            var users = new List<User>();
            foreach (var file in files)
            {
                if (file.Contains("~")) continue;
                var userPrection = new ExcelReader().GetResult(file);
                users.Add(userPrection);
            }

            return users;
        }
    }
}
