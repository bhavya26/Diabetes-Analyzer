using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using DiabetesManager.Models;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Uwp;
using Windows.Services.Store;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Services.Store.Engagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GraphPage : Page
    {

        public ObservableCollection<DbManager> Graph1;
        public List<DbManager> Graph;
        private StoreContext context = null;
        private PrintHelper _printHelper;

        public GraphPage()
        {
            this.InitializeComponent();
            GetLicenseInfo();

        }
        
        private void HistoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Item1.IsSelected)
            {
                Graph = GraphData.GetRecord();
                LineChart.ItemsSource = Graph;
                LineChart1.ItemsSource = Graph;
                GlucoseText.Text = "All";
            }
            else if (Item2.IsSelected)
            {
                Graph = GraphData1.GetRecord1();
                LineChart.ItemsSource = Graph;
                LineChart1.ItemsSource = Graph;
                GlucoseText.Text = "Breakfast";
            }
            else if (Item3.IsSelected)
            {
                Graph = GraphData2.GetRecord2();
                LineChart.ItemsSource = Graph;
                LineChart1.ItemsSource = Graph;
                GlucoseText.Text = "Lunch";
            }
            else if (Item4.IsSelected)
            {
                Graph = GraphData3.GetRecord3();
                LineChart.ItemsSource = Graph;
                LineChart1.ItemsSource = Graph;
                GlucoseText.Text = "Dinner";
            }

        }
        private async void LockButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProMode Show1 = new ProMode();
            await Show1.ShowAsync();
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
                    LockButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    PrintButton1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }

                // Use members of the addOnLicense object to access license info
                // for the add-on...
            }

        }

        private async void PrintButton1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StoreServicesCustomEventLogger logger = StoreServicesCustomEventLogger.GetDefault();
            logger.Log("GraphPrint");
            PrintGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await Task.Delay(1);
            PrintGrid.Children.Remove(PrintStack);



            _printHelper = new PrintHelper(Container);

            _printHelper.AddFrameworkElementToPrint(PrintStack);

            _printHelper.OnPrintFailed += PrintHelper_OnPrintFailed;

            _printHelper.OnPrintSucceeded += PrintHelper_OnPrintSucceeded;


            await _printHelper.ShowPrintUIAsync("Diabetes Analyzer");

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
            Frame.Navigate(typeof(GraphPage));
        }



        private async void PrintHelper_OnPrintFailed()

        {

            ReleasePrintHelper();

            var dialog = new MessageDialog("Printing failed.");

                await dialog.ShowAsync();
            Frame.Navigate(typeof(GraphPage));

        }

        private void PrintHelper_OnPrintCanceled()

        {

            ReleasePrintHelper();

        }

       
    }
}
