
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DiabetesManager.Models;
using Windows.Storage;
using Windows.Storage.Pickers;
using Microsoft.Services.Store.Engagement;
using System.Reflection;
using Syncfusion.Pdf;
using System.Drawing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DiabetesManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfExport : Page
    {
        string path;
        SQLite.SQLiteConnection conn;
        public Nullable<DateTimeOffset> Date { get; set; }
        public String FromCal { get; set; }
        public String ToCal { get; set; }
        public DateTime FromCalender;
        public DateTime ToCalender;
        public PdfExport()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
        }

        private async void FromDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            try
            {
                FromCalender = Convert.ToDateTime(FromDate.Date.ToString());

            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Date Already Selected");
                await dialog.ShowAsync();
            }
        }

        private async void ToDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            try
            {
                ToCalender = Convert.ToDateTime(ToDate.Date.ToString());

            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Date Already Selected");
                await dialog.ShowAsync();
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StoreServicesCustomEventLogger logger = StoreServicesCustomEventLogger.GetDefault();
                logger.Log("PdfGenerate");
                MessageDialog dialog = new MessageDialog("Please Select the Path where you want to Save the File and the Name of File");
                await dialog.ShowAsync();
                GeneratePdf(FromCalender, ToCalender);

            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Something Went Wrong");
                await dialog.ShowAsync();
            }
        }

        private async void GeneratePdf(DateTime FromDate, DateTime ToDate) //async
        {
            try
            {


                PdfDocument doc = new PdfDocument();


                //MessageDialog dialog = new MessageDialog("PdfDocument Initialized");
                //await dialog.ShowAsync();
                //Add a page.

                PdfPage page = doc.Pages.Add();

                RectangleF bounds = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

                PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

                //Load the image as stream

                // FileStream imageStream = new FileStream("Assets\\square44x44logo.scale-100.png", FileAccess.Read);

                // FileStream imageStream = new FileStream("AppX\\Assets\\square44x44logo.scale-100.png", FileMode.Open, FileAccess.Read);

                //    StorageFile sFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Assets\Square150x150Logo.scale-100.png");

                //    MessageDialog dialog1 = new MessageDialog("picture added");
                //    await dialog1.ShowAsync();

                //    var imageStream = await sFile.OpenStreamForReadAsync();
                //PdfImage image = new PdfBitmap(imageStream);

                //Draw the image in the header.

                // header.Graphics.DrawImage(image, new PointF(0, 0), new SizeF(50, 50));
                header.Graphics.DrawString(PdfName.Text + "\n" + PdfNumber.Text + "\n" + PdfSocial.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 11), PdfBrushes.Black, new PointF(100, 0));
                //Add the header at the top.

                doc.Template.Top = header;

                //List of Columns 



               
                var people = (from p in conn.Table<DbManager>()
                              where p.Date1 >= FromDate && p.Date1 <= ToDate
                              select new
                              {
                                  p.Glucose,
                                  Meal = p.Reading,
                                  Meal_Type = p.Reading1,
                                  p.Date,
                                  p.Time,
                                  p.Comments,
                                  p.Date1
                              }).OrderBy(x => x.Date1).ToList();

                if (people.Count > 0)
                    {
                        PdfGraphics graphics = page.Graphics;

                        //Set the standard font.

                        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                        //Draw the text.

                        graphics.DrawString("Diabetes Analyzer Data", font, PdfBrushes.Black, new PointF(30, 20));
                        // Create a PdfLightTable. 
                        PdfLightTable pdfLightTable = new PdfLightTable();
                        pdfLightTable.Style.ShowHeader = true;

                        //Assign data source. 
                        pdfLightTable.DataSource = people;
                        PdfLightTableLayoutFormat layoutFormat = new PdfLightTableLayoutFormat();

                        layoutFormat.Break = PdfLayoutBreakType.FitPage;

                        layoutFormat.Layout = PdfLayoutType.Paginate;

                        //Draw PdfLightTable.
                        pdfLightTable.Style.CellPadding = 10;
                        pdfLightTable.Draw(page, new PointF(30, 70), layoutFormat);
                        //Draw PdfLightTable. 
                        //pdfLightTable.Draw(page, new PointF(30, 70));

                        //Save the document.
                        MemoryStream stream = new MemoryStream();

                        //Saves the PDF document to stream
                        await doc.SaveAsync(stream);
                        // doc.Save("Output.pdf");

                        //Close the document

                        doc.Close(true);
                        //MessageDialog dialog3 = new MessageDialog("Succes retrieved");
                        //await dialog3.ShowAsync();
                        Save(stream, "Result.pdf");

                    }
                    else
                    {
                        MessageDialog dialog5 = new MessageDialog("No Data for Selected Date Range");
                        await dialog5.ShowAsync();
                    }
                
               


                //MessageDialog dialog2 = new MessageDialog("Data retrieved");
                //await dialog2.ShowAsync();



            }
            catch (Exception ex)
            {
                MessageDialog dialog1 = new MessageDialog(ex.ToString());
                await dialog1.ShowAsync();
            }
        }

        async void Save(Stream stream, string filename)
        {
            try
            {

                stream.Position = 0;

                StorageFile stFile;
                if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons")))
                {
                    FileSavePicker savePicker = new FileSavePicker();
                    savePicker.DefaultFileExtension = ".pdf";
                    savePicker.SuggestedFileName = "DiabetesAnalyzerPdf";
                    savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
                    stFile = await savePicker.PickSaveFileAsync();
                }
                else
                {
                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                    stFile = await local.CreateFileAsync(filename, CreationCollisionOption.GenerateUniqueName);
                }
                if (stFile != null)
                {
                    Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
                    Stream st = fileStream.AsStreamForWrite();
                    st.Write((stream as MemoryStream).ToArray(), 0, (int)stream.Length);
                    st.Flush();
                    st.Dispose();
                    fileStream.Dispose();
                    MessageDialog dialog1 = new MessageDialog("Pdf Generated Successfully");
                    await dialog1.ShowAsync();
                }
               
            }
            catch (Exception)
            {
                MessageDialog dialog1 = new MessageDialog("Some Error Occurred. Please Try Again");
                await dialog1.ShowAsync();
            }





        }
    }
}
