﻿using HeroesParserData.Properties;
using NLog;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace HeroesParserData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger DatabaseCopyLog = LogManager.GetLogger("DatabaseCopyLogFile");

        public static bool UpdateInProgress { get; set; }
        public static string NewLatestDirectory { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            UpdateInProgress = false;

            InitDatabase();

            // set default location
            if (string.IsNullOrEmpty(Settings.Default.ReplaysLocation))
                Settings.Default.ReplaysLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Heroes of the Storm\Accounts");

            // add custom accent and theme resource dictionaries
            //ThemeManager.AddAccent("Theme", new Uri("pack://application:,,,/Resources/Theme.xaml"));

            //// get the theme from the current application
            //var theme = ThemeManager.DetectAppStyle(Application.Current);

            //// now use the custom accent
            //ThemeManager.ChangeAppStyle(Application.Current,
            //                        ThemeManager.GetAccent("Theme"),
            //                        theme.Item1);

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Default.Save();

            if (UpdateInProgress && !string.IsNullOrEmpty(NewLatestDirectory))
            {
                SqlConnection.ClearAllPools();
                CopyDatabaseToLatestRelease();
            }

            base.OnExit(e);
        }

        private void InitDatabase()
        {
            string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
            string databasePath = Path.Combine(applicationPath, "Database");

            if (!Directory.Exists(databasePath))
                Directory.CreateDirectory(databasePath);
                
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
        }

        private void CopyDatabaseToLatestRelease()
        {
            string dbFile = "HeroesParserData.db";
            string dbFilePath = @"Database\HeroesParserData.db";
            string newAppDirectory = Path.Combine(NewLatestDirectory, "Database");

            try
            {
                if (!File.Exists(dbFilePath))
                {
                    DatabaseCopyLog.Log(LogLevel.Info, $"Database file not found: {dbFilePath}");
                    DatabaseCopyLog.Log(LogLevel.Info, "Nothing to copy, update completed");

                    return;
                }

                Directory.CreateDirectory(newAppDirectory);
                DatabaseCopyLog.Log(LogLevel.Info, $"Directory created: {newAppDirectory}");

                File.Copy(dbFilePath, Path.Combine(newAppDirectory, dbFile));

                DatabaseCopyLog.Log(LogLevel.Info, $"Database file copied to: {Path.Combine(newAppDirectory, dbFile)}");
                DatabaseCopyLog.Log(LogLevel.Info, "Update completed");
            }
            catch (Exception ex)
            {
                DatabaseCopyLog.Log(LogLevel.Info, ex);
            }
        }
    }
}
