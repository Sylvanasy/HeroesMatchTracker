﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using HeroesMatchTracker.Core.Messaging;
using HeroesMatchTracker.Data;

namespace HeroesMatchTracker.Core.Models.ReplayModels
{
    public class ParserCheckboxes : ObservableObject
    {
        public const int LatestParsedIndex = 0;
        public const int LastParsedIndex = 1;
        public const int LatestHotsLogsUploaderIndex = 2;
        public const int LastHotsLogsUploaderIndex = 3;

        private IDatabaseService Database;

        private bool[] ScanDateTimeCheckboxes = new bool[4] { false, false, false, false };

        public ParserCheckboxes(IDatabaseService database)
        {
            Database = database;

            ScanDateTimeCheckboxes[Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex] = true;

            Messenger.Default.Register<NotificationMessage>(this, (message) => ReceivedMessage(message));
        }

        public bool LatestParsedChecked
        {
            get => ScanDateTimeCheckboxes[LatestParsedIndex];
            set
            {
                if (Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex != LatestParsedIndex)
                {
                    ScanDateTimeCheckboxes[LatestParsedIndex] = value;
                    if (value)
                    {
                        Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex = LatestParsedIndex;
                        LastParsedChecked = false;
                        LatestHotsLogsUploaderChecked = false;
                        LastHotsLogsUploaderChecked = false;
                    }
                }

                RaisePropertyChanged();
            }
        }

        public bool LastParsedChecked
        {
            get => ScanDateTimeCheckboxes[LastParsedIndex];
            set
            {
                if (Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex != LastParsedIndex)
                {
                    ScanDateTimeCheckboxes[LastParsedIndex] = value;
                    if (value)
                    {
                        Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex = LastParsedIndex;
                        LatestParsedChecked = false;
                        LatestHotsLogsUploaderChecked = false;
                        LastHotsLogsUploaderChecked = false;
                    }
                }

                RaisePropertyChanged();
            }
        }

        public bool LatestHotsLogsUploaderChecked
        {
            get => ScanDateTimeCheckboxes[LatestHotsLogsUploaderIndex];
            set
            {
                if (Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex != LatestHotsLogsUploaderIndex)
                {
                    ScanDateTimeCheckboxes[LatestHotsLogsUploaderIndex] = value;
                    if (value)
                    {
                        Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex = LatestHotsLogsUploaderIndex;
                        LatestParsedChecked = false;
                        LastParsedChecked = false;
                        LastHotsLogsUploaderChecked = false;
                    }
                }

                RaisePropertyChanged();
            }
        }

        public bool LastHotsLogsUploaderChecked
        {
            get => ScanDateTimeCheckboxes[LastHotsLogsUploaderIndex];
            set
            {
                if (Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex != LastHotsLogsUploaderIndex)
                {
                    ScanDateTimeCheckboxes[LastHotsLogsUploaderIndex] = value;
                    if (value)
                    {
                        Database.SettingsDb().UserSettings.SelectedScanDateTimeIndex = LastHotsLogsUploaderIndex;
                        LatestParsedChecked = false;
                        LastParsedChecked = false;
                        LatestHotsLogsUploaderChecked = false;
                    }
                }

                RaisePropertyChanged();
            }
        }

        private void ReceivedMessage(NotificationMessage message)
        {
            if (message.Notification == StaticMessage.HotsLogsUploaderDisabled)
            {
                if (LatestHotsLogsUploaderChecked || LastHotsLogsUploaderChecked)
                    LatestParsedChecked = true;

                LatestHotsLogsUploaderChecked = false;
                LastHotsLogsUploaderChecked = false;
            }
        }
    }
}
