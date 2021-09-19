using System;
using System.Collections.Generic;
using Windows.Foundation.Metadata;
using Windows.Services.Store;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using DiabetesManager.Helpers;
using Windows.Storage.AccessCache;
using Windows.Storage;
//using Microsoft.Advertising.WinRT.UI;
using AdDealsUniversalSDKW81;
using AdDealsUniversalSDKW81.Views.UserControls;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StoreContext context = null;
        int x1, x2;
       
       
        private bool Check = true;
        int count = 0;

        public MainPage()
        {
            this.InitializeComponent();
            MainGrid.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            MainGrid.ManipulationStarted += MainGrid_ManipulationStarted;
            MainGrid.ManipulationCompleted += MainGrid_ManipulationCompleted;
            DiabetesFrame.Navigate(typeof(HomePage));
            HomeList.IsSelected = true;
            reviewfunction();
            GetLicenseInfo();
            WhatsNew();
            




            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = Colors.Black;
                    statusBar.ForegroundColor = Colors.White;
                }
            }

            AdManager.InitSDK(this.MainGrid, "3250", "Q7ASM59CPIHV");
            BroadcastConsent(true);

            

        }

        private async void testAds()
        {
            //AdDealsPopupAd popupAdToShow = await AdManager.GetPopupAd(this.MainGrid, AdManager.AdKind.FULLSCREENPOPUPAD);
            //popupAdToShow.ShowAd();
            //AdDealsPopupAd cachePopupAd = await AdManager.GetPopupAd(this.MainGrid, AdManager.AdKind.FULLSCREENPOPUPAD); 
            //await cachePopupAd.CacheAd();
            //cachePopupAd.CacheAdSuccess -= CacheAdSuccess_Event;

            AdDealsPopupAd popupAdToShow = await AdManager.GetPopupAd(this.MainGrid, AdManager.AdKind.FULLSCREENPOPUPAD);
            popupAdToShow.ShowAd();

        }

        private async void CacheAdSuccess_Event(object sender, EventArgs e)
        {
            AdDealsPopupAd popupAdToShow = await AdManager.GetPopupAd(this.MainGrid, AdManager.AdKind.FULLSCREENPOPUPAD); 
            popupAdToShow.ShowAd();

        }

         

        private void BroadcastConsent(bool? agreement) 
            // Intended to be invoked from a method devoted to broadcast across various third-party SDKs you may integrate
        { AdManager.PrivacyPolicyConsent adDealsConsent; 
            if (!agreement.HasValue) 
                // the current user doesn't fall under the GPRD law or any other Data Protection law worldwide 
            { 
                adDealsConsent = AdManager.PrivacyPolicyConsent.NOT_APPLICABLE; 
            } else 
            { 
                if (agreement.Value == true) 
                { 
                    adDealsConsent = AdManager.PrivacyPolicyConsent.GRANT; 
                } 
                else 
                { 
                    adDealsConsent = AdManager.PrivacyPolicyConsent.REVOKE; 
                } 
            } 
            AdManager.SetConsent(adDealsConsent); 
        }

        


        private void MainGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            x2 = (int)e.Position.Y;
            if (x1 > x2)
            {
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
            else
            {
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
        }

        private void MainGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            x1 = (int)e.Position.X;
        }


        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProModeList.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                ProMode Show = new ProMode();
                await Show.ShowAsync();
            }

            if (HomeList.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(HomePage));
                TitleTextBlock.Text = "Home";
                Split.IsPaneOpen = !Split.IsPaneOpen;
                
            }
            else if (HistoryList.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(HistoryPage));
                TitleTextBlock.Text = "History";
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
            else if (GraphList.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(GraphPage));
                TitleTextBlock.Text = "Graphs";
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
            else if (StatList.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(StatsPage));
                TitleTextBlock.Text = "Statistics";
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
            else if (AboutList.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(AboutPage));
                TitleTextBlock.Text = "About Us";
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
            else if (ExcelUpload.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(HomePage));
                //TitleTextBlock.Text = "About Us";
                TitleTextBlock.Text = "Home";
                string Appname = "Diabetes Analyzer";
                MessageDialog mydial1 = new MessageDialog("Please Upload the Excel that was generated from the app itself.\n If you want to upload your own data than please upload in the same format as the one generated by the app ");
                mydial1.Title = Appname;
                await mydial1.ShowAsync();
                Excel_Upload _Upload = new Excel_Upload();
                 await _Upload.Upload();



                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
            else if (PdfExport.IsSelected)
            {
                count++;
                if (Check == true)
                {
                    if (count % 2 == 0)
                        testAds();
                }
                DiabetesFrame.Navigate(typeof(PdfExport));
                TitleTextBlock.Text = "Generate Pdf";
                Split.IsPaneOpen = !Split.IsPaneOpen;
            }
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            Split.IsPaneOpen = !Split.IsPaneOpen;
            if (Check == true)
            {
                //if (InterstitialAdState.Ready == myInterstitialAd.State)
                //{
                //    myInterstitialAd.Show();
                //}
            }
        }

        public async void reviewfunction()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //set the app name
            string Appname = "Diabetes Analyzer";

            if (!settings.Values.ContainsKey("review"))
            {
                settings.Values.Add("review", 1);
                settings.Values.Add("rcheck", 0);
            }
            else
            {
                int no = Convert.ToInt32(settings.Values["review"]);
                int check = Convert.ToInt32(settings.Values["rcheck"]);
                no++;
                if ((no % 5 == 0) && check == 0)
                {
                    settings.Values["review"] = no;
                    MessageDialog mydial = new MessageDialog("Thank you for using this application.\nWould you like to give some time to rate and review this application to help us improve");
                    mydial.Title = Appname;
                    mydial.Commands.Add(new UICommand(
                        "Yes",
                        new UICommandInvokedHandler(this.CommandInvokedHandler_yesclick)));
                    mydial.Commands.Add(new UICommand(
                       "No",
                       new UICommandInvokedHandler(this.CommandInvokedHandler_noclick)));
                    await mydial.ShowAsync();

                }
                else
                {
                    settings.Values["review"] = no;
                }
            }
        }

        public async void WhatsNew()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //set the app name


            if (!settings.Values.ContainsKey("X6"))
            {
                settings.Values.Add("X6", 1);
                settings.Values.Remove("X5");


                int no = Convert.ToInt32(settings.Values["X6"]);

                if ((no == 1))
                {
                    settings.Values["X6"] = no;
                    WhatsNew WhatsNewDialog = new WhatsNew();
                    var userResult = await WhatsNewDialog.ShowAsync();
                }
                else
                {
                    settings.Values["X6"] = no;
                }
            }


        }
        private void CommandInvokedHandler_noclick(IUICommand command)
        {

        }

        private async void CommandInvokedHandler_yesclick(IUICommand command)
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            settings.Values["rcheck"] = 1;

            //add your app id here
            var StoreUri = new Uri(@"ms-windows-store://review/?ProductId=9NBLGGH4Q3QR");
            var Success = await Windows.System.Launcher.LaunchUriAsync(StoreUri);
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
                // textBlock.Text = "An error occurred while retrieving the license.";
                return;
            }

            // Use members of the appLicense object to access license info...

            // Access the add on licenses for add-ons for this app.
            foreach (KeyValuePair<string, StoreLicense> item in appLicense.AddOnLicenses)
            {
                StoreLicense addOnLicense = item.Value;
                if (addOnLicense.IsActive)
                {
                    ProModeList.Visibility = Visibility.Collapsed;
                    ExcelUpload.Visibility = Visibility.Visible;
                    PdfExport.Visibility = Visibility.Visible;
                    Check = false;
                }
            }
        }
    }
}
