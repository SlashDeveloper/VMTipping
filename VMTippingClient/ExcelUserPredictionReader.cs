using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using VMTipping.Model;

namespace VMTippingClient
{
    public class ExcelUserPredictionReader
    {
        public async Task<User> GetUserPrectionFromWorkbook(Workbook workbook)
        {
            var predictionSheet = GetSheet(workbook, "Gruppespilltipp");
            var endgameSheets = GetSheet(workbook, "Sluttspilltipp");
            var user = new User
            {
                Name = predictionSheet.Range["B3"].Value2,
                MatchPredictions = GetMatchPredictions(predictionSheet),
                RoundOf16 = GetTeamsInRange(endgameSheets, 4, 7),
                RoundOf8 = GetTeamsInRange(endgameSheets, 11, 12),
                RoundOf4 = GetTeamsInRange(endgameSheets, 16, 16),
                Ranking = GetTeamsInRange(endgameSheets, 20, 20),
            };

            return user;
        }

        private IList<Team> GetTeamsInRange(Worksheet sheet, int startRow, int endRow)
        {
            var teams = new List<Team>();

            for (int currentRow = startRow; currentRow <= endRow; currentRow++)
            {
                var team1 = new Team() { Name = sheet.Range["A" + currentRow].Value };
                var team2 = new Team() { Name = sheet.Range["B" + currentRow].Value };
                var team3 = new Team() { Name = sheet.Range["C" + currentRow].Value };
                var team4 = new Team() { Name = sheet.Range["D" + currentRow].Value };
                teams.Add(team1);
                teams.Add(team2);
                teams.Add(team3);
                teams.Add(team4);
            }
            return teams;

        }

        private IList<MatchPrediction> GetMatchPredictions(Worksheet predictionSheet)
        {
            var matchPredictions = new List<MatchPrediction>();
            const int startRow = 11;

            var matchId = 1;
            for (int currentRow = startRow; currentRow <= 40; currentRow++)
            {

                if (predictionSheet.Range["A" + currentRow].Value == null || predictionSheet.Range["A" + currentRow].Value == "" || predictionSheet.Range["A" + currentRow].Value.ToString().Contains("Group")) continue;
                var matchPrediction = new MatchPrediction
                {
                    MatchId = matchId,
                    HomeTeam = new Team
                    {
                        Name = predictionSheet.Range["A" + currentRow].Value
                    },
                    AwayTeam = new Team
                    {
                        Name = predictionSheet.Range["B" + currentRow].Value
                    },
                    HomeGoals = Convert.ToInt32(predictionSheet.Range["C" + currentRow].Value),
                    AwayGoals = Convert.ToInt32(predictionSheet.Range["D" + currentRow].Value)
                };
                matchPredictions.Add(matchPrediction);
                matchId += 1;

                // GROUP E-H
                matchPrediction = new MatchPrediction
               {
                   MatchId = matchId,
                   HomeTeam = new Team
                   {
                       Name = predictionSheet.Range["G" + currentRow].Value
                   },
                   AwayTeam = new Team
                   {
                       Name = predictionSheet.Range["H" + currentRow].Value
                   },
                   HomeGoals = Convert.ToInt32(predictionSheet.Range["I" + currentRow].Value),
                   AwayGoals = Convert.ToInt32(predictionSheet.Range["J" + currentRow].Value)
               };

                matchPredictions.Add(matchPrediction);
                matchId += 1;

            }


            return matchPredictions;
        }

        public Worksheet GetSheet(Workbook workbook, string name)
        {
            foreach (Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name.ToUpper() == name.ToUpper())
                {
                    return sheet;
                }
            }
            return null;
        }
    }
}

