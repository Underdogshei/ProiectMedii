using System;
using ProiectMedii.Data;
using System.IO;

namespace ProiectMedii
{
    public partial class App : Application
    {
        static ServiceListDatabase database;

        public static ServiceListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ServiceListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ServiceList.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
