using DiabetesManager.Models;
using System;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

using System.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatsPage : Page
    {
        string path;
        SQLite.SQLiteConnection conn;
        string D = "Dinner";
        string L = "Lunch";
        string B = "Breakfast";
        public StatsPage()
        {
            this.InitializeComponent();

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);

            SwipePop();
        }

        public async void SwipePop()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //set the app name


            if (!settings.Values.ContainsKey("aa"))
            {
                settings.Values.Add("aa", 1);

                int no = Convert.ToInt32(settings.Values["aa"]);

                if ((no == 1))
                {
                    settings.Values["aa"] = no;
                    SwipeDialog SwipeDialog = new SwipeDialog();
                    var userResult = await SwipeDialog.ShowAsync();
                }
                else
                {
                    settings.Values["aa"] = no;
                }
            }


        }


        private async void StatsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                //DateTime TodaysDate = DateTime.Today;
                //String Td = String.Format("{0:d}", TodaysDate);
                //DateTime LastWeekDate = TodaysDate.AddDays(-7);
                //String Ld = String.Format("{0:d}", LastWeekDate);
                //DateTime PastMonthDate = TodaysDate.AddDays(-30);
                //DateTime PastThreeDate = TodaysDate.AddDays(-90);
                //DateTime PastSixDate = TodaysDate.AddDays(-180);
                //DateTime PastYearDate = TodaysDate.AddDays(-365);


                //if (Item1.IsSelected)
                //{

                //    var query = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager");
                //    foreach (var item in query)
                //    {

                //        RadialGauge.Value = Double.Parse(item.Min);
                //        RadialGauge1.Value = Double.Parse(item.Max);
                //        RadialGauge2.Value = Double.Parse(item.Avg);
                //    }
                //}
                //else if (Item2.IsSelected)
                //{
                //    var query1 = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager as Db Where Db.Date1>=?", LastWeekDate);

                //    foreach (var item in query1)
                //    {
                //        RadialGauge.Value = Double.Parse(item.Min);
                //        RadialGauge1.Value = Double.Parse(item.Max);
                //        RadialGauge2.Value = Double.Parse(item.Avg);

                //    }
                //}
                //else if (Item3.IsSelected)
                //{
                //    var query1 = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager as Db Where Db.Date1>=?", PastMonthDate);

                //    foreach (var item in query1)
                //    {
                //        RadialGauge.Value = Double.Parse(item.Min);
                //        RadialGauge1.Value = Double.Parse(item.Max);
                //        RadialGauge2.Value = Double.Parse(item.Avg);

                //    }

                //}
                //else if (Item4.IsSelected)
                //{
                //    var query1 = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager as Db Where Db.Date1>=?", PastThreeDate);

                //    foreach (var item in query1)
                //    {
                //        RadialGauge.Value = Double.Parse(item.Min);
                //        RadialGauge1.Value = Double.Parse(item.Max);
                //        RadialGauge2.Value = Double.Parse(item.Avg);

                //    }
                //}
                //else if (Item5.IsSelected)
                //{
                //    var query1 = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager as Db Where Db.Date1>=?", PastSixDate);

                //    foreach (var item in query1)
                //    {
                //        RadialGauge.Value = Double.Parse(item.Min);
                //        RadialGauge1.Value = Double.Parse(item.Max);
                //        RadialGauge2.Value = Double.Parse(item.Avg);

                //    }
                //}
                //else if (Item6.IsSelected)
                //{
                //    var query1 = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager as Db Where Db.Date1>=?", PastYearDate);

                //    foreach (var item in query1)
                //    {
                //        RadialGauge.Value = Double.Parse(item.Min);
                //        RadialGauge1.Value = Double.Parse(item.Max);
                //        RadialGauge2.Value = Double.Parse(item.Avg);

                //    }
                //}
                if (Item1.IsSelected || Item2.IsSelected || Item3.IsSelected || Item4.IsSelected || Item5.IsSelected || Item6.IsSelected)
                {
                    StatsCombo1.SelectedItem = StatsCombo1.FindName("All");
                }
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("The error is due to the fact that only new data you add the statistics would be shown\n Sorry for the Inconveniance");
                await dialog.ShowAsync();
            }

        }

        private async void StatsCombo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime TodaysDate = DateTime.Today;
            String Td = String.Format("{0:d}", TodaysDate);
            DateTime LastWeekDate = TodaysDate.AddDays(-7);
            String Ld = String.Format("{0:d}", LastWeekDate);
            DateTime PastMonthDate = TodaysDate.AddDays(-30);
            DateTime PastThreeDate = TodaysDate.AddDays(-90);
            DateTime PastSixDate = TodaysDate.AddDays(-180);
            DateTime PastYearDate = TodaysDate.AddDays(-365);
            try
            {
                if (Item1.IsSelected)
                {
                    if (All.IsSelected)
                    {
                        var query = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager");
                        foreach (var item in query)
                        {

                            RadialGauge.Value = Double.Parse(item.Min);
                            RadialGauge1.Value = Double.Parse(item.Max);
                            RadialGauge2.Value = Double.Parse(item.Avg);
                        }

                    }
                    else if (Breakfast.IsSelected)
                    {
                        AllTimeData(B);
                    }
                    else if (Lunch.IsSelected)
                    {
                        AllTimeData(L);
                    }
                    else if (Dinner.IsSelected)
                    {
                        AllTimeData(D);
                    }

                }
                if (Item2.IsSelected)
                {
                    if (All.IsSelected)
                    {
                        AllData(LastWeekDate);
                    }
                    else if (Breakfast.IsSelected)
                    {
                        Data(B, LastWeekDate);

                    }
                    else if (Lunch.IsSelected)
                    {
                        Data(L, LastWeekDate);

                    }
                    else if (Dinner.IsSelected)
                    {
                        Data(D, LastWeekDate);
                    }
                }
                if (Item3.IsSelected)
                {
                    if (All.IsSelected)
                    {
                        AllData(PastMonthDate);
                    }
                    else if (Breakfast.IsSelected)
                    {
                        Data(B, PastMonthDate);

                    }
                    else if (Lunch.IsSelected)
                    {
                        Data(L, PastMonthDate);

                    }
                    else if (Dinner.IsSelected)
                    {
                        Data(D, PastMonthDate);
                    }
                }
                if (Item4.IsSelected)
                {
                    if (All.IsSelected)
                    {
                        AllData(PastThreeDate);
                    }
                    else if (Breakfast.IsSelected)
                    {
                        Data(B, PastThreeDate);

                    }
                    else if (Lunch.IsSelected)
                    {
                        Data(L, PastThreeDate);

                    }
                    else if (Dinner.IsSelected)
                    {
                        Data(D, PastThreeDate);
                    }
                }
                if (Item5.IsSelected)
                {
                    if (All.IsSelected)
                    {
                        AllData(PastSixDate);
                    }
                    else if (Breakfast.IsSelected)
                    {
                        Data(B, PastSixDate);

                    }
                    else if (Lunch.IsSelected)
                    {
                        Data(L, PastSixDate);

                    }
                    else if (Dinner.IsSelected)
                    {
                        Data(D, PastSixDate);
                    }
                }
                if (Item6.IsSelected)
                {
                    if (All.IsSelected)
                    {
                        AllData(PastYearDate);
                    }
                    else if (Breakfast.IsSelected)
                    {
                        Data(B, PastYearDate);

                    }
                    else if (Lunch.IsSelected)
                    {
                        Data(L, PastYearDate);

                    }
                    else if (Dinner.IsSelected)
                    {
                        Data(D, PastYearDate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.ToString());
                await dialog.ShowAsync();
            }
        }
        
        public async void AllTimeData(string Data)
        {
            try
            {
                var query = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager where Reading=?", Data);
                foreach (var item in query)
                {

                    RadialGauge.Value = Double.Parse(item.Min);
                    RadialGauge1.Value = Double.Parse(item.Max);
                    RadialGauge2.Value = Double.Parse(item.Avg);
                }
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("No Relevant Data Stored Please Store Data First");
                await dialog.ShowAsync();
            }
        }
        public async void AllData(DateTime Time)
        {
            try
            {
                var query = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager Db where Db.Date1>=?", Time);
                foreach (var item in query)
                {

                    RadialGauge.Value = Double.Parse(item.Min);
                    RadialGauge1.Value = Double.Parse(item.Max);
                    RadialGauge2.Value = Double.Parse(item.Avg);
                }
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("No Relevant Data Stored Please Store Data First");
                await dialog.ShowAsync();
            }
        }
        public async void Data(string Data, DateTime Time)
        {
            try
            {
                var query = conn.Query<DbManager>("Select Min(Glucose) as Min, Max(Glucose) as Max, Avg(Glucose) as Avg from DbManager Db where Db.Reading=? and Db.Date1>=?", Data, Time);
                foreach (var item in query)
                {

                    RadialGauge.Value = Double.Parse(item.Min);
                    RadialGauge1.Value = Double.Parse(item.Max);
                    RadialGauge2.Value = Double.Parse(item.Avg);
                }
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("No Relevant Data Stored Please Store Data First");
                await dialog.ShowAsync();
            }
        }


    }

    

}


