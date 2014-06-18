using System;
using System.Collections.Generic;
using System.Linq;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class ResultService
    {
        IList<Game> games = new List<Game>(); 
        public ResultService()
        {
            games.Add(new Game
            {
                Id = 1,
                HomeTeam = new Team("Brazil"),
                AwayTeam = new Team("Croatia"),
                Date = new DateTime(2014, 6, 12, 20, 0,0),
                HomeGoals = 3,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 2,
                HomeTeam = new Team("Mexico"),
                AwayTeam = new Team("Cameroon"),
                Date = new DateTime(2014, 6, 13, 18, 0, 0),
                HomeGoals = 1,
                AwayGoals = 0,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 3,
                HomeTeam = new Team("Spain"),
                AwayTeam = new Team("Netherlands"),
                Date = new DateTime(2014, 6, 13, 21, 0, 0),
                HomeGoals = 1,
                AwayGoals = 5,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 4,
                HomeTeam = new Team("Chile"),
                AwayTeam = new Team("Australia"),
                Date = new DateTime(2014, 6, 14, 0, 0, 0),
                HomeGoals = 3,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 5,
                HomeTeam = new Team("Colombia"),
                AwayTeam = new Team("Greece"),
                Date = new DateTime(2014, 6, 14, 18, 0, 0),
                HomeGoals = 3,
                AwayGoals = 0,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 6,
                HomeTeam = new Team("Uruguay"),
                AwayTeam = new Team("Costa Rica"),
                Date = new DateTime(2014, 6, 14, 21, 0, 0),
                HomeGoals = 1,
                AwayGoals = 3,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 7,
                HomeTeam = new Team("England"),
                AwayTeam = new Team("Italy"),
                Date = new DateTime(2014, 6, 15, 0, 0, 0),
                HomeGoals = 1,
                AwayGoals = 2,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 8,
                HomeTeam = new Team("Côte d'Ivoire"),
                AwayTeam = new Team("Japan"),
                Date = new DateTime(2014, 6, 15, 3, 0, 0),
                HomeGoals = 2,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 9,
                HomeTeam = new Team("Switzerland"),
                AwayTeam = new Team("Ecuador"),
                Date = new DateTime(2014, 6, 15, 18, 0, 0),
                HomeGoals = 2,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 10,
                HomeTeam = new Team("France"),
                AwayTeam = new Team("Honduras"),
                Date = new DateTime(2014, 6, 15, 21, 0, 0),
                HomeGoals = 3,
                AwayGoals = 0,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 11,
                HomeTeam = new Team("Argentina"),
                AwayTeam = new Team("Bosnia"),
                Date = new DateTime(2014, 6, 16, 0, 0, 0),
                HomeGoals = 2,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 12,
                HomeTeam = new Team("Germany"),
                AwayTeam = new Team("Portugal"),
                Date = new DateTime(2014, 6, 16, 18, 0, 0),
                HomeGoals = 4,
                AwayGoals = 0,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 13,
                HomeTeam = new Team("Iran"),
                AwayTeam = new Team("Nigeria"),
                Date = new DateTime(2014, 6, 16, 21, 0, 0),
                HomeGoals = 0,
                AwayGoals = 0,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 14,
                HomeTeam = new Team("USA"),
                AwayTeam = new Team("Ghana"),
                Date = new DateTime(2014, 6, 17, 0, 0, 0),
                HomeGoals = 2,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 15,
                HomeTeam = new Team("Belgium"),
                AwayTeam = new Team("Algeria"),
                Date = new DateTime(2014, 6, 17, 18, 0, 0),
                HomeGoals = 2,
                AwayGoals = 1,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 16,
                HomeTeam = new Team("Brazil"),
                AwayTeam = new Team("Mexico"),
                Date = new DateTime(2014, 6, 17, 21, 0, 0),
                HomeGoals = 0,
                AwayGoals = 0,
                IsPlayed = true
            });
            games.Add(new Game
            {
                Id = 17,
                HomeTeam = new Team("Russia"),
                AwayTeam = new Team("Korea Republic"),
                Date = new DateTime(2014, 6, 18, 0, 0, 0),
                HomeGoals = 1,
                AwayGoals = 1,
                IsPlayed = true
            });
            
        }

        public IList<Game> GetGamesThatArePlayed()
        {
            return games.Where(g => g.IsPlayed).ToList();
        } 
    }
}
