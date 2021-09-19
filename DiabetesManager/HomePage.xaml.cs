using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DiabetesManager.Models;
using Windows.UI.Popups;

using Windows.System.Profile;
using Windows.Services.Store;
using System.Collections.Generic;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class HomePage : Page
    {
        string path;
        //SQLite.Net.SQLiteConnection conn;
        SQLite.SQLiteConnection SQLiteConnection;
        public Nullable<DateTimeOffset> Date { get; set; }
        public String Fcombo { get; set; }
        public String Scombo { get; set; }
        public String Cal { get; set; }
        public String Cal1 { get; set; }
        public String GlucoseValue { get; set; }
        public float GlucoColor { get; set; }
        public float GValue { get; set; }
        public string TimeValue { get; set; }
        public string g;
        public string m;
        public DateTime Calender1;
        private StoreContext context = null;
        private bool Check = true;

       

       

        public HomePage()
        {
            this.InitializeComponent();


            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            SQLiteConnection = new SQLite.SQLiteConnection(path);
            SQLiteConnection.CreateTable<DbManager>();
            
            GetLicenseInfo();
            
            
           
           

        }

       


        public async void GetLicenseInfo()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }


            StoreAppLicense appLicense = await context.GetAppLicenseAsync();


            if (appLicense == null)
            {
                //textBlock.Text = "An error occurred while retrieving the license.";
                return;
            }

            // Use members of the appLicense object to access license info...

            // Access the add on licenses for add-ons for this app.
            foreach (KeyValuePair<string, StoreLicense> item in appLicense.AddOnLicenses)
            {
                StoreLicense addOnLicense = item.Value;


                if (addOnLicense.InAppOfferToken.Equals("CsvExport"))
                {
                    if (addOnLicense.IsActive)
                    {
                        //AdUnit.Visibility = Visibility.Collapsed;
                        CommentsStack.Visibility = Visibility.Visible;
                        Check = false;
                    }
                }

                if (addOnLicense.InAppOfferToken.Equals("RemoveAds"))
                {
                    if (addOnLicense.IsActive)
                    {
                        //AdUnit.Visibility = Visibility.Collapsed;
                        Check = false;
                    }

                }

                // Use members of the addOnLicense object to access license info
                // for the add-on...
            }

        }


        private async void Calender_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            try
            {
                Cal = Calender.Date.ToString();
                Calender1 = Convert.ToDateTime(Cal);

            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Date Already Selected");
                await dialog.ShowAsync();
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var Combo1 = (ComboBox)sender;
            var Item1 = (ComboBoxItem)Combo1.SelectedItem;
            g = Item1.Content.ToString();

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Combo = (ComboBox)sender;
            var Item = (ComboBoxItem)Combo.SelectedItem;
            Fcombo = Item.Content.ToString();
            if (Breakfast_Combo.IsSelected)
            {

                Break_Combo1.IsSelected = true;
                Lunch_Combo1.Visibility = Visibility.Collapsed;
                Lunch_Combo2.Visibility = Visibility.Collapsed;
                Break_Combo1.Visibility = Visibility.Visible;
                Break_Combo2.Visibility = Visibility.Visible;
                Dinner_Combo1.Visibility = Visibility.Collapsed;
                Dinner_Combo2.Visibility = Visibility.Collapsed;

            }

            if (Lunch_Combo.IsSelected)
            {
                Breakfast_Combo.IsSelected = false;
                Lunch_Combo1.IsSelected = true;
                Dinner_Combo1.IsSelected = false;
                Lunch_Combo1.Visibility = Visibility.Visible;
                Lunch_Combo2.Visibility = Visibility.Visible;
                Break_Combo1.Visibility = Visibility.Collapsed;
                Break_Combo2.Visibility = Visibility.Collapsed;
                Dinner_Combo1.Visibility = Visibility.Collapsed;
                Dinner_Combo2.Visibility = Visibility.Collapsed;
            }
            else
            if (Dinner_Combo.IsSelected)
            {
                Breakfast_Combo.IsSelected = false;
                Lunch_Combo1.IsSelected = false;
                Dinner_Combo1.IsSelected = true;
                Lunch_Combo1.Visibility = Visibility.Collapsed;
                Lunch_Combo2.Visibility = Visibility.Collapsed;
                Break_Combo1.Visibility = Visibility.Collapsed;
                Break_Combo2.Visibility = Visibility.Collapsed;
                Dinner_Combo1.Visibility = Visibility.Visible;
                Dinner_Combo2.Visibility = Visibility.Visible;
            }


        }


        private void GlucoseTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlucoseValue = GlucoseTextBlock.Text;


        }
        private void TimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            //TimeValue = TimePicker.Time.ToString();

        }
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlucoColor = float.Parse(GlucoseValue);
                Scombo = g;
                Cal1 = Cal.Substring(0, 10);
                string TimeString = TimePicker.Time.ToString();
                if (Scombo == "Before Breakfast" || Scombo == "Před snídaní" || Scombo == "Vor dem Frühstück"
                    || Scombo == "Avant le petit déjeuner" || Scombo == "Prima colazione"
                    || Scombo == "Voor het ontbijt" || Scombo == "Antes do pequeno almoço"
                     || Scombo == "Pred raňajkami" || Scombo == "早餐前")
                {
                    GValue = 1;
                }
                else if (Scombo == "After Breakfast" || Scombo == "Po snídani" || Scombo == "Nach dem Frühstück"
                    || Scombo == "Après le petit déjeuner" || Scombo == "Dopo la prima colazione"
                     || Scombo == "Na het ontbijt" || Scombo == "Depois do pequeno almoço"
                     || Scombo == "Po raňajkách" || Scombo == "早餐后")
                {
                    GValue = 2;
                }
                if (Scombo == "Before Lunch" || Scombo == "Před obědem" || Scombo == "Vor dem Mittagessen"
                    || Scombo == "Avant le déjeuner" || Scombo == "Prima di pranzo"
                     || Scombo == "Voor de Lunch" || Scombo == "Antes do almoço"
                      || Scombo == "Pred obedom" || Scombo == "午饭前")
                {
                    GValue = 1;
                }
                else if (Scombo == "After Lunch" || Scombo == "Po obědě" || Scombo == "Nach dem Mittagessen"
                    || Scombo == "Après le déjeuner" || Scombo == "Dopo il pranzo"
                     || Scombo == "Na de Lunch" || Scombo == "Depois do almoço"
                      || Scombo == "Po obede" || Scombo == "午饭后")
                {
                    GValue = 2;
                }
                if (Scombo == "Before Dinner" || Scombo == "Před večeří" || Scombo == "Vor dem Abendessen"
                    || Scombo == "Avant le dîner" || Scombo == "Prima di cena"
                     || Scombo == "Voor het diner" || Scombo == "Antes do jantar"
                      || Scombo == "Pred večerou" || Scombo == "在晚餐前")
                {
                    GValue = 1;
                }
                else if (Scombo == "After Dinner" || Scombo == "Po večeři" || Scombo == "Nach dem Abendessen"
                    || Scombo == "Après le dîner" || Scombo == "Dopo cena"
                     || Scombo == "Na het diner" || Scombo == "Depois do jantar"
                      || Scombo == "Po večeri" || Scombo == "吃过晚饭后")
                {
                    GValue = 2;
                }
                SQLiteConnection.Insert(new DbManager()
                {
                    Glucose = float.Parse(GlucoseValue),
                    Reading = Fcombo,
                    Reading1 = Scombo,
                    Date = Cal1,
                    Time = TimeString.ToString(),
                    Value = GValue,
                    Date1 = Calender1,
                    Comments = Comments.Text
                });

               
               

                if (Check == true)
                {
                    //if (InterstitialAdState.Ready == myInterstitialAd1.State)
                    //{
                    //    myInterstitialAd1.Show();
                    //}
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.ToString() + "\nError Please Try Again");
                await dialog.ShowAsync();
            }
        }

        private void InsideFlyout_Click(object sender, RoutedEventArgs e)
        {

            SubmitFlyout.Hide();
        }


    }


}
