using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using VMTipping.Model;

namespace VMTippingClient
{
    public class MainViewModel
    {
        public ICommand ReadFilesCommand { get; set; }
        public ICommand SaveCommand{ get; set; }
        public ObservableCollection<User> Users { get; set; }

        public MainViewModel()
        {   
            ReadFilesCommand = new RelayCommand(() => Users = new ObservableCollection<User>(new FileReader().ReadPredictionFiles()));
            SaveCommand = new RelayCommand(SaveToFile);
        }

        private void SaveToFile()
        {
            // Configure save file dialog box
            var dlg = new SaveFileDialog
            {
                FileName = "Users",
                DefaultExt = ".json",
                Filter = "Json files (.json)|*.json"
            };

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                string json = JsonConvert.SerializeObject(Users, Formatting.Indented);
                File.WriteAllText(filename, json);
            }
            
        }
    }
}