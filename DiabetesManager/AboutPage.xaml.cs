using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Email;
using Windows.Services.Store;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        
        private StoreContext context = null;
        public AboutPage()
        {
            this.InitializeComponent();
            GetLicenseInfo();

        }

       

        private async void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            EmailMessage email = new EmailMessage();
            email.To.Add(new EmailRecipient("bhavya26@hotmail.com"));
            email.Subject = "Diabetes Manager Feedback";
            await EmailManager.ShowComposeNewEmailAsync(email);
        }

        private async void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            var StoreUri = new Uri(@"ms-windows-store://review/?ProductId=9NBLGGH4Q3QR");
            var Success = await Windows.System.Launcher.LaunchUriAsync(StoreUri);
            if (Success)
            {

            }
            else
            {

            }
        }

        private void RemoveAdv_Click(object sender, RoutedEventArgs e)
        {
            String Id = "9nblggh4sdcb";
            PurchaseAddOn(Id);

        }
           

        private void SmallDonation_Click(object sender, RoutedEventArgs e)
        {
            PurchaseAddOn("9nblggh4sccp");
          //  ConsumeAddOn("9nblggh4sccp");
        }


        private void BigDonation_Click(object sender, RoutedEventArgs e)
        {

           // ConsumeAddOn("9nblggh4scd3");
            PurchaseAddOn("9nblggh4scd3");
        }

        public async void PurchaseAddOn(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
           
            }

          //  workingProgressRing.IsActive = true;
            StorePurchaseResult result = await context.RequestPurchaseAsync(storeId);
          //  workingProgressRing.IsActive = false;

            if (result.ExtendedError != null)
            {
             
                return;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased: 
                    MessageDialog showDialog = new MessageDialog("Aleady Purchased");
                    await showDialog.ShowAsync();
                    break;

                case StorePurchaseStatus.Succeeded:
                    MessageDialog showDialog1 = new MessageDialog("Purchase Successfull");
                    await showDialog1.ShowAsync();
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
        public async void ConsumeAddOn(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();

            }

            // This is an example for a Store-managed consumable, where you specify the actual number
            // of units that you want to report as consumed so the Store can update the remaining
            // balance. For a developer-managed consumable where you maintain the balance, specify 1
            // to just report the add-on as fulfilled to the Store.
            uint quantity = 1;
            //string addOnStoreId = "9nblggh4scd3";
            Guid trackingId = Guid.NewGuid();


            StoreConsumableResult result = await context.ReportConsumableFulfillmentAsync(
                storeId, quantity, trackingId);


            if (result.ExtendedError != null)
            {
                // The user may be offline or there might be some other server failure.
                // textBlock.Text = $"ExtendedError: {result.ExtendedError.Message}";
                return;
            }

            switch (result.Status)
            {
                case StoreConsumableStatus.Succeeded:
                    MessageDialog showDialog1 = new MessageDialog("Purchase Successfull");
                    await showDialog1.ShowAsync();
                    break;

                case StoreConsumableStatus.NetworkError:
                    MessageDialog showDialog3 = new MessageDialog("Network Error");
                    await showDialog3.ShowAsync();
                    break;

                case StoreConsumableStatus.ServerError:
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
                // textBlock.Text = "An error occurred while retrieving the license.";
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
                    }
                }

                if (addOnLicense.InAppOfferToken.Equals("RemoveAds"))
                {
                    if (addOnLicense.IsActive)
                    {
                        //AdUnit.Visibility = Visibility.Collapsed;
                    }

                }
            }
        }

        private async void ProMode_Click(object sender, RoutedEventArgs e)
        {
            
            ProMode Show = new ProMode();
            await Show.ShowAsync();

        }

        public async void PurchaseAddOn1(string storeId)
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();

            }

            //  workingProgressRing.IsActive = true;
            StorePurchaseResult result = await context.RequestPurchaseAsync(storeId);
            //  workingProgressRing.IsActive = false;

            if (result.ExtendedError != null)
            {

                return;
            }

            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    MessageDialog showDialog = new MessageDialog("Aleady Purchased");
                    await showDialog.ShowAsync();
                    break;

                case StorePurchaseStatus.Succeeded:
                    MessageDialog showDialog1 = new MessageDialog("Purchase Successfull\n The App Needs to Restarted");
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
    }
}