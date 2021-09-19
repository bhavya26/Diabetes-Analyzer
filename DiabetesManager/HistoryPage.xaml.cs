
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
//using SQLite.Net;
//using SQLite.Net.Platform.WinRT;
using DiabetesManager.Models;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.Services.Store;
using Microsoft.Toolkit.Uwp.Helpers;


using Windows.Storage;
using Windows.Storage.Pickers;
using System.Data;
using Microsoft.Services.Store.Engagement;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryPage : Page
    {

        string path;
        SQLite.SQLiteConnection conn;
        public List<DbManager> Manage;
        public List<DbManager> Manage1;
        public List<DbManager> DelList;
        private StoreContext context = null;
        private PrintHelper _printHelper;
      
        private bool Check = true;

        public HistoryPage()
        {
          
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
          
            HistoryPopUp();
            GetLicenseInfo();
           
        }

        



        public async void HistoryPopUp()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //set the app name


            if (!settings.Values.ContainsKey("H1"))
            {
                settings.Values.Add("H1", 1);

                int no = Convert.ToInt32(settings.Values["H1"]);

                if ((no == 1))
                {
                    settings.Values["H1"] = no;
                    HistoryPop History = new HistoryPop();
                    await History.ShowAsync();
                }
                else
                {
                    settings.Values["H1"] = no;
                }
            }
        }

        private static SQLite.SQLiteConnection DbConnection
        {
            get
            {
                return new SQLite.SQLiteConnection(
                   
                    Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite"));
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Item1.IsSelected)
            {
                //Manage = DataManager.GetRecord();
                Manage1 = DataManager1.GetRecord();


                BreakfastList.ItemsSource = Manage1;


            }
            else if (Item2.IsSelected)
            {
                Manage = BreakfastData.GetRecord1();

                BreakfastList.ItemsSource = Manage;
            }
            else if (Item3.IsSelected)
            {
                Manage = LunchData.GetRecord2();
                BreakfastList.ItemsSource = Manage;

            }
            else if (Item4.IsSelected)
            {
                Manage = DinnerData.GetRecord3();
                BreakfastList.ItemsSource = Manage;

            }
            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd2.State)
                //{
                //    myInterstitialAd2.Show();
                //}
            }

        }
        private void SelectList_Click(object sender, RoutedEventArgs e)
        {
            if (BreakfastList.SelectionMode == Windows.UI.Xaml.Controls.ListViewSelectionMode.Single)
                BreakfastList.SelectionMode = Windows.UI.Xaml.Controls.ListViewSelectionMode.Multiple;
            else if (BreakfastList.SelectionMode == Windows.UI.Xaml.Controls.ListViewSelectionMode.Multiple)
                BreakfastList.SelectionMode = Windows.UI.Xaml.Controls.ListViewSelectionMode.Single;
            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd2.State)
                //{
                //    myInterstitialAd2.Show();
                //}
            }

        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var query = BreakfastList.SelectedItems;
           
                foreach (var item in query)
                {
                    //dbContext.DbManager.Remove((DbManager)item);
                    conn.Delete(item);

                }
              
            

            if (Item1.IsSelected)
            {
               
                    List<DbManager> people = (from p in conn.Table<DbManager>()
                                              select p).OrderByDescending(q => q.id).ToList();
                BreakfastList.ItemsSource = people;
                    //Manage1 = DataManager1.GetRecord();
                    //BreakfastList.ItemsSource = Manage1;
                

            }
            else
            if (Item2.IsSelected)
            {
                List<DbManager> people = conn.Table<DbManager>().OrderByDescending(q => q.id).Where(q => q.Reading == "Breakfast").ToList();

            }
            else if (Item3.IsSelected)
            {
                List<DbManager> people = conn.Table<DbManager>().OrderByDescending(q => q.id).Where(q => q.Reading == "Lunch").ToList();

            }
            else if (Item4.IsSelected)
            {
                List<DbManager> people = conn.Table<DbManager>().OrderByDescending(q => q.id).Where(q => q.Reading == "Dinner").ToList();

            }
            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd2.State)
                //{
                //    myInterstitialAd2.Show();
                //}
            }

        }

        private async void ExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            ProMode Show = new ProMode();
            await Show.ShowAsync();
            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd2.State)
                //{
                //    myInterstitialAd2.Show();
                //}
            }

        }
        public async void PurchaseAddOn(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            //  workingProgressRing.IsActive = true;
            StorePurchaseResult result = await context.RequestPurchaseAsync(storeId);
            //  workingProgressRing.IsActive = false;

            if (result.ExtendedError != null)
            {
                // The user may be offline or there might be some other server failure.
                //  textBlock.Text = $"ExtendedError: {result.ExtendedError.Message}";
                return;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    MessageDialog showDialog = new MessageDialog("Aleady Purchased");
                    await showDialog.ShowAsync();
                    break;

                case StorePurchaseStatus.Succeeded:
                    MessageDialog showDialog1 = new MessageDialog("Purchase Successfull\n App needs to be Restarted to Enable the Functionality");
                    await showDialog1.ShowAsync();
                    Application.Current.Exit();
                    break;

                case StorePurchaseStatus.NotPurchased:
                    MessageDialog showDialog2 = new MessageDialog("Not Successfull");
                    await showDialog2.ShowAsync();
                    break;

                case StorePurchaseStatus.NetworkError:
                    MessageDialog showDialog3 = new MessageDialog("Network Error");
                    await showDialog3.ShowAsync();
                    break;

                case StorePurchaseStatus.ServerError:
                    MessageDialog showDialog4 = new MessageDialog("Server Error");
                    await showDialog4.ShowAsync();
                    break;

                default:
                    MessageDialog showDialog5 = new MessageDialog("Unknown Error");
                    await showDialog5.ShowAsync();
                    break;
            }
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


                if (addOnLicense.IsActive)
                {
                    ExportToCsv.Visibility = Visibility.Collapsed;
                    UnlockedCsv.Visibility = Visibility.Visible;
                    EditDetails.Visibility = Visibility.Visible;
                    PrintButton.Visibility = Visibility.Visible;
                    Check = false;
                }

                // Use members of the addOnLicense object to access license info
                // for the add-on...
            }

        }

        private async void UnlockedCsv_Click(object sender, RoutedEventArgs e)
        {
            StoreServicesCustomEventLogger logger = StoreServicesCustomEventLogger.GetDefault();
            logger.Log("ExportCsv");
            var csv = new CsvExport<DbManager>(DataManager1.GetRecord());
            csv.ExportToFile("DiabetesAnalyzer.csv");
            MessageDialog showDialog5 = new MessageDialog("Please Open Download Folder To Open the File");
            await showDialog5.ShowAsync();
            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd2.State)
                //{
                //    myInterstitialAd2.Show();
                //}
            }

            //string path= "C:\\Users\\admin\\Downloads\\4715bhavyaxshah.DiabetesAnalyzer_xea4f2thhdtw0!App";
            //StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            //await Launcher.LaunchFolderAsync(folder);
        }

        private void OuterGridStack_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            DbManager dm = sp.DataContext as DbManager;
            if (dm != null)
            {
                //if (dm.Glucose >= 150)
                //    sp.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                //else if (dm.Glucose < 150)
                //    sp.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                if (dm.Value == 1 && dm.Glucose <= 130)
                {

                    sp.Background = new SolidColorBrush(Windows.UI.Colors.LimeGreen);
                }
                else if (dm.Value == 1 && dm.Glucose > 130)
                {
                    sp.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (dm.Value == 2 && dm.Glucose <= 150)
                {
                    sp.Background = new SolidColorBrush(Windows.UI.Colors.LimeGreen);
                }
                else if (dm.Value == 2 && dm.Glucose > 150)
                {
                    sp.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                }
            }
        }

        private async void EditDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Data = BreakfastList.SelectedItem as DbManager;
                Frame.Navigate(typeof(EditPage), Data);
            }
            catch (Exception)
            {
                MessageDialog showDialog5 = new MessageDialog("Please Select Only One Data");
                await showDialog5.ShowAsync();
            }
        }

        private async void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            //Shell.Current.DisplayWaitRing = true;

            StoreServicesCustomEventLogger logger = StoreServicesCustomEventLogger.GetDefault();
            logger.Log("HistoryPrint");

            PrintGrid.Children.Remove(PrintStack);



            _printHelper = new PrintHelper(Container);

            _printHelper.AddFrameworkElementToPrint(PrintStack);

            _printHelper.OnPrintFailed += PrintHelper_OnPrintFailed;

            _printHelper.OnPrintSucceeded += PrintHelper_OnPrintSucceeded;


            await _printHelper.ShowPrintUIAsync("Diabetes Analyzer");

            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd2.State)
                //{
                //    myInterstitialAd2.Show();
                //}
            }
        }

        private void ReleasePrintHelper()

        {

            _printHelper.Dispose();



            if (PrintGrid.Children.Contains(PrintStack))

            {

                PrintGrid.Children.Add(PrintStack);

            }



            //Shell.Current.DisplayWaitRing = false;

        }



        private async void PrintHelper_OnPrintSucceeded()

        {

            ReleasePrintHelper();

            var dialog = new MessageDialog("Printing done.");

            await dialog.ShowAsync();
            Frame.Navigate(typeof(HistoryPage));
        }



        private async void PrintHelper_OnPrintFailed()

        {

            ReleasePrintHelper();

            var dialog = new MessageDialog("Printing failed.");

            await dialog.ShowAsync();
            Frame.Navigate(typeof(HistoryPage));

        }

        private void PrintHelper_OnPrintCanceled()

        {

            ReleasePrintHelper();

        }

        

    }
}

