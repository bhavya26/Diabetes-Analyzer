using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Store;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    public sealed partial class ProMode : ContentDialog
    {
        private StoreContext context = null;
        public ProMode()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            PurchaseAddOn("9nblggh4trvh");
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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
    }
}
