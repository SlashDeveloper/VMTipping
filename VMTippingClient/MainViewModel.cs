using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using VMTipping.Model;

namespace VMTippingClient
{
    public class MainViewModel: ViewModelBase
    {      
        public ICommand ReadFilesCommand { get; set; }
        public ICommand SaveCommand{ get; set; }
        public ICommand ReadUserJsonCommand { get; set; } 
        public ObservableCollection<User> Users { get; set; }
        //public ObservableCollection<Score> Ranking { get; set; }
        public ObservableCollection<Match> Matches { get; set; }

        public MainViewModel()
        {
            Users = new ObservableCollection<User>();
            //Ranking = new ObservableCollection<Score>();
            //Ranking = new ObservableCollection<Score>();
            ReadFilesCommand = new RelayCommand(ReadFiles);
            SaveCommand = new RelayCommand(SaveToFile);
            ReadUserJsonCommand = new RelayCommand(GetUsersFromJson);
        }

        private void GetUsersFromJson()
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".json";
            dlg.Filter = "Json files (.json)|*.json";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                var users = JsonConvert.DeserializeObject<IList<User>>(File.ReadAllText(filename));
                foreach (var user in users)
                {
                    Users.Add(user);
                }
                UpdateRanking();
            }


        }

        

        private void UpdateRanking()
        {
            //Ranking.Clear();
            //var games = new ResultService().GetGamesThatArePlayed();
            //var ranking = new ScoreService().GetRanking(Users, games);
            //foreach (var score in ranking)
            //{
            //    Ranking.Add(score);
            //}
        }

        private async void ReadFiles()
        {
            var files = await new FileReader().ReadPredictionFiles();
            foreach (var file in files)
            {
                var userPrection = await new ExcelReader().GetResult(file);
                Users.Add(userPrection);
                await Task.Run(() => Thread.Sleep(100));
            }
            
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
