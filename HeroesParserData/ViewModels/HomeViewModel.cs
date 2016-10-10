﻿using GalaSoft.MvvmLight.Messaging;
using Heroes.ReplayParser;
using HeroesParserData.DataQueries;
using HeroesParserData.Messages;
using HeroesParserData.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeroesParserData.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private BitmapImage _backgroundMapImage;
        private Color _labelGlowColor;
        private long _replaysInDatabase;
        private DateTime _latestUploadedReplay;
        private DateTime _lastUploadedReplay;
        private int _totalQuickMatchGames;
        private int _totalUnrankedDraftGames;
        private int _totalHeroLeagueGames;
        private int _totalTeamLeagueGames;
        private int _totalCustomGames;
        private int _seasonQuickMatchGames;
        private int _seasonUnrankedDraftGames;
        private int _seasonHeroLeagueGames;
        private int _seasonTeamLeagueGames;
        private int _seasonCustomGames;
        private int _seasonTotal;
        private string _selectedSeason;
        private bool IsRefreshDataOn;

        private List<string> _seasonList = new List<string>();
        private List<Tuple<BitmapImage, Color>> ListOfBackgroundImages;

        #region public properties
        public BitmapImage BackgroundMapImage
        {
            get { return _backgroundMapImage; }
            set
            {
                _backgroundMapImage = value;
                RaisePropertyChangedEvent(nameof(BackgroundMapImage));
            }
        }

        public Color LabelGlowColor
        {
            get { return _labelGlowColor; }
            set
            {
                _labelGlowColor = value;
                RaisePropertyChangedEvent(nameof(LabelGlowColor));
            }
        }

        public long ReplaysInDatabase
        {
            get { return _replaysInDatabase; }
            set
            {
                _replaysInDatabase = value;
                RaisePropertyChangedEvent(nameof(ReplaysInDatabase));
            }
        }

        public DateTime LatestUploadedReplay
        {
            get { return _latestUploadedReplay; }
            set
            {
                _latestUploadedReplay = value;
                RaisePropertyChangedEvent(nameof(LatestUploadedReplay));
            }
        }

        public DateTime LastUploadedReplay
        {
            get { return _lastUploadedReplay; }
            set
            {
                _lastUploadedReplay = value;
                RaisePropertyChangedEvent(nameof(LastUploadedReplay));
            }
        }

        public int TotalQuickMatchGames
        {
            get { return _totalQuickMatchGames; }
            set
            {
                _totalQuickMatchGames = value;
                RaisePropertyChangedEvent(nameof(TotalQuickMatchGames));
            }
        }

        public int TotalUnrankedDraftGames
        {
            get { return _totalUnrankedDraftGames; }
            set
            {
                _totalUnrankedDraftGames = value;
                RaisePropertyChangedEvent(nameof(TotalUnrankedDraftGames));
            }
        }

        public int TotalHeroLeagueGames
        {
            get { return _totalHeroLeagueGames; }
            set
            {
                _totalHeroLeagueGames = value;
                RaisePropertyChangedEvent(nameof(TotalHeroLeagueGames));
            }
        }

        public int TotalTeamLeagueGames
        {
            get { return _totalTeamLeagueGames; }
            set
            {
                _totalTeamLeagueGames = value;
                RaisePropertyChangedEvent(nameof(TotalTeamLeagueGames));
            }
        }

        public int TotalCustomGames
        {
            get { return _totalCustomGames; }
            set
            {
                _totalCustomGames = value;
                RaisePropertyChangedEvent(nameof(TotalCustomGames));
            }
        }

        public int SeasonQuickMatchGames
        {
            get { return _seasonQuickMatchGames; }
            set
            {
                _seasonQuickMatchGames = value;
                RaisePropertyChangedEvent(nameof(SeasonQuickMatchGames));
            }
        }

        public int SeasonUnrankedDraftGames
        {
            get { return _seasonUnrankedDraftGames; }
            set
            {
                _seasonUnrankedDraftGames = value;
                RaisePropertyChangedEvent(nameof(SeasonUnrankedDraftGames));
            }
        }

        public int SeasonHeroLeagueGames
        {
            get { return _seasonHeroLeagueGames; }
            set
            {
                _seasonHeroLeagueGames = value;
                RaisePropertyChangedEvent(nameof(SeasonHeroLeagueGames));
            }
        }

        public int SeasonTeamLeagueGames
        {
            get { return _seasonTeamLeagueGames; }
            set
            {
                _seasonTeamLeagueGames = value;
                RaisePropertyChangedEvent(nameof(SeasonTeamLeagueGames));
            }
        }

        public int SeasonCustomGames
        {
            get { return _seasonCustomGames; }
            set
            {
                _seasonCustomGames = value;
                RaisePropertyChangedEvent(nameof(SeasonCustomGames));
            }
        }

        public string SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
                _selectedSeason = value;
                SetSeasonStats();
                RaisePropertyChangedEvent(nameof(SelectedSeason));
            }
        }

        public List<string> SeasonList
        {
            get { return _seasonList;}
            set
            {
                _seasonList = value;
                RaisePropertyChangedEvent(nameof(SeasonList));
            }
        }

        public int SeasonTotal
        {
            get { return _seasonTotal; }
            set
            {
                _seasonTotal = value;
                RaisePropertyChangedEvent(nameof(SeasonTotal));
            }
        }
        #endregion public properties

        public HomeViewModel()
            :base()
        {
            Messenger.Default.Register<HomeTabMessage>(this, (action) => ReceiveMessage(action));
            SetBackgroundImages();
            SetRandomBackgroundImage();
            SetSeasonList();
            SetData();
        }

        private void SetBackgroundImages()
        {
            string uri = "pack://application:,,,/HeroesIcons;component/Icons/Homescreens/";

            ListOfBackgroundImages = new List<Tuple<BitmapImage, Color>>();
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_alarak.jpg"), UriKind.Absolute)), Colors.Purple));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_chromie.jpg"), UriKind.Absolute)), Colors.Gold));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_diablo.jpg"), UriKind.Absolute)), Colors.Red));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_diablotristram.jpg"), UriKind.Absolute)), Colors.Gray));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_eternalconflict.jpg"), UriKind.Absolute)), Colors.DarkRed));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_eternalconflict_dark.jpg"), UriKind.Absolute)), Colors.DarkRed));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_greymane.jpg"), UriKind.Absolute)), Colors.LightBlue));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_guldan.jpg"), UriKind.Absolute)), Colors.Green));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_lunara.jpg"), UriKind.Absolute)), Colors.Purple));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_lunarnewyear.jpg"), UriKind.Absolute)), Colors.Purple));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_medivh.jpg"), UriKind.Absolute)), Colors.Gray));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_nexus.jpg"), UriKind.Absolute)), Colors.Purple));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_overwatchhangar.jpg"), UriKind.Absolute)), Colors.Gray));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_shrines.jpg"), UriKind.Absolute)), Colors.Red));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_shrines_dusk.jpg"), UriKind.Absolute)), Colors.Red));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_starcraft.jpg"), UriKind.Absolute)), Colors.DarkBlue));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_starcraft_protoss.jpg"), UriKind.Absolute)), Colors.Cyan));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_starcraft_zerg.jpg"), UriKind.Absolute)), Colors.DarkRed));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_varian.jpg"), UriKind.Absolute)), Colors.Red));
            ListOfBackgroundImages.Add(new Tuple<BitmapImage, Color>(new BitmapImage(new Uri(string.Concat(uri, "storm_ui_homescreenbackground_zarya.jpg"), UriKind.Absolute)), Colors.Purple));
        }

        private void SetRandomBackgroundImage()
        {
            Random random = new Random();
            int num = random.Next(0, ListOfBackgroundImages.Count);
            BackgroundMapImage = ListOfBackgroundImages[num].Item1;
            LabelGlowColor = ListOfBackgroundImages[num].Item2;
        }

        private void SetSeasonList()
        {
            SeasonList = Utilities.GetSeasonList();
            SelectedSeason = Settings.Default.SelectedSeason;
        }

        private void SetData()
        {
            ReplaysInDatabase = Query.Replay.GetTotalReplayCount();
            LatestUploadedReplay = Query.Replay.ReadLatestReplayByDateTime();
            LastUploadedReplay = Query.Replay.ReadLastReplayByDateTime();
            TotalQuickMatchGames = Query.Replay.ReadTotalGames(GameMode.QuickMatch);
            TotalUnrankedDraftGames = Query.Replay.ReadTotalGames(GameMode.UnrankedDraft);
            TotalHeroLeagueGames = Query.Replay.ReadTotalGames(GameMode.HeroLeague);
            TotalTeamLeagueGames = Query.Replay.ReadTotalGames(GameMode.TeamLeague);
            TotalCustomGames = Query.Replay.ReadTotalGames(GameMode.Custom);

            SetSeasonStats();
        }

        private void SetSeasonStats()
        {
            Season season = Utilities.GetSeasonFromString(SelectedSeason);
            SeasonQuickMatchGames = Query.Replay.ReadTotalGamesForSeason(GameMode.QuickMatch, season);
            SeasonUnrankedDraftGames = Query.Replay.ReadTotalGamesForSeason(GameMode.UnrankedDraft, season);
            SeasonHeroLeagueGames = Query.Replay.ReadTotalGamesForSeason(GameMode.HeroLeague, season);
            SeasonTeamLeagueGames = Query.Replay.ReadTotalGamesForSeason(GameMode.TeamLeague, season);
            SeasonCustomGames = Query.Replay.ReadTotalGamesForSeason(GameMode.Custom, season);
            SeasonTotal = SeasonQuickMatchGames + SeasonUnrankedDraftGames + SeasonHeroLeagueGames + SeasonTeamLeagueGames + SeasonCustomGames;
        }

        private void ReceiveMessage(HomeTabMessage action)
        {
            if (action.Trigger == Trigger.Update)
            {
                if (!IsRefreshDataOn)
                {
                    IsRefreshDataOn = true;

                    Task.Run(async () =>
                    {
                        SetData();
                        while (App.IsProcessingReplays)
                        {
                            await Task.Delay(3000);
                            SetData();
                        }
                        IsRefreshDataOn = false;
                    });
                }
            }
        }
    }
}