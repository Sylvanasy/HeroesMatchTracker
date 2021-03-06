﻿using HeroesMatchTracker.Data;
using HeroesMatchTracker.Data.Models.Replays;
using System.Collections.Generic;
using System.Linq;

namespace HeroesMatchTracker.Core.Models.MatchModels
{
    public class MatchResult
    {
        private IDatabaseService Database;

        public MatchResult(IDatabaseService database)
        {
            Database = database;

            TeamBlue = "Team Blue";
            TeamRed = "Team Red";
        }

        public int? TeamBlueKills { get; private set; }
        public int? TeamRedKills { get; private set; }
        public int? TeamBlueLevel { get; private set; }
        public int? TeamRedLevel { get; private set; }
        public string TeamBlue { get; private set; }
        public string TeamRed { get; private set; }
        public string TeamBlueIsWinner { get; private set; }
        public string TeamRedIsWinner { get; private set; }

        public void SetResult(List<MatchPlayerStats> team1PlayerStats, List<MatchPlayerStats> team2PlayerStats, List<ReplayMatchTeamLevel> matchTeamLevelList, List<ReplayMatchPlayer> matchPlayerList)
        {
            long userPlayerId = Database.SettingsDb().UserSettings.UserPlayerId;

            if (userPlayerId > 0)
            {
                foreach (var player in matchPlayerList)
                {
                    if (player.PlayerId == userPlayerId)
                    {
                        if (player.Team == 0)
                        {
                            TeamBlue = "Your Team";
                            TeamRed = "Enemy Team";
                        }
                        else if (player.Team == 1)
                        {
                            TeamBlue = "Enemy Team";
                            TeamRed = "Your Team";
                        }

                        break;
                    }
                }
            }

            TeamBlueKills = team1PlayerStats.Sum(x => x.SoloKills);
            TeamRedKills = team2PlayerStats.Sum(x => x.SoloKills);

            TeamBlueLevel = matchTeamLevelList.Max(x => x.Team0Level);
            TeamRedLevel = matchTeamLevelList.Max(x => x.Team1Level);

            if (matchPlayerList[0].IsWinner)
            {
                TeamBlueIsWinner = "(Winner)";
                TeamRedIsWinner = "(Loser)";
            }
            else
            {
                TeamRedIsWinner = "(Winner)";
                TeamBlueIsWinner = "(Loser)";
            }
        }
    }
}
