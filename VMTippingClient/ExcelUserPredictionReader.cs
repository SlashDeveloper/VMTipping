using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using VMTipping.Model;

namespace VMTippingClient
{
    public class ExcelUserPredictionReader
    {
        public User GetUserPrectionFromWorkbook(Workbook workbook)
        {
            var predictionSheet = GetSheet(workbook, "Gruppespilltipp");
            return new User
            {
                Name = predictionSheet.Range["B3"].Value2,
                MatchPredictions = GetMatchPredictions(predictionSheet)
            };
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
                    } ,
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
