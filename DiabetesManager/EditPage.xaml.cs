using DiabetesManager.Models;
using System;
using System.IO;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPage : Page
    {
        string path;
        SQLite.SQLiteConnection conn;
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
        public DateTime Calender2;
        public int Id1;

        public EditPage()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            //conn.CreateTable<DbManager>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var Data = e.Parameter as DbManager;
                Id1 = Data.id;
            }
            catch (Exception)
            {
                MessageDialog showDialog5 = new MessageDialog("No Data Selected Please Go Back");
                await showDialog5.ShowAsync();
                Frame.Navigate(typeof(HistoryPage));
            }

        }
        private void GlucoseTextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlucoseValue = GlucoseTextBlock1.Text;
        }

        private void Combo1_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void Combo2_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Combo1 = (ComboBox)sender;
            var Item1 = (ComboBoxItem)Combo1.SelectedItem;
            g = Item1.Content.ToString();
        }

        private void Calender1_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            Cal = Calender1.Date.ToString();
            Calender2 = Convert.ToDateTime(Cal);
        }

        private void TimePicker1_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {

        }

        private async void SubmitButton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlucoColor = float.Parse(GlucoseValue);
                Scombo = g;
                Cal1 = Cal.Substring(0, 10);
                string TimeString = TimePicker1.Time.ToString();
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
                float G = float.Parse(GlucoseValue);
                string T = TimeString.ToString();

                var query = conn.Table<DbManager>().Where(z => z.id == Id1);
                foreach (var data in query)
                {
                    data.Glucose = G;
                    data.Reading = Fcombo;
                    data.Reading1 = Scombo;
                    data.Date = Cal1;
                    data.Time = T;
                    data.Value = GValue;
                    data.Date1 = Calender2;
                    data.Comments = Comments1.Text;
                    conn.Update(data);
                }



            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.ToString() + "\nError Please Try Again");
                await dialog.ShowAsync();
            }
        }

        private void InsideFlyout1_Click(object sender, RoutedEventArgs e)
        {
            SubmitFlyout.Hide();
            Frame.Navigate(typeof(HistoryPage));
        }
    }
}
