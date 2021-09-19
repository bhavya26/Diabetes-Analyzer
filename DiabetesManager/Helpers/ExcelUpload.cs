using DiabetesManager.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace DiabetesManager.Helpers
{
    class Excel_Upload
    {

        public async Task Upload()
        {
            string path;
            SQLite.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            openPicker.FileTypeFilter.Add(".csv");


            StorageFile file = await openPicker.PickSingleFileAsync();
            //string token= StorageApplicationPermissions.FutureAccessList.Add(file);
            // var data = await file.OpenStreamForReadAsync();
            // var x=await StorageApplicationPermissions.FutureAccessList.GetFileAsync(token);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //var stream = File.Open(x.Path, FileMode.Open, FileAccess.Read)
            if (file != null)
            {
                try
                {
                    using (Stream stream = await file.OpenStreamForReadAsync())
                    {

                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx)
                        using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                        {

                            // Choose one of either 1 or 2:

                            // 1. Use the reader methods
                            do
                            {
                                while (reader.Read())
                                {
                                    // reader.GetDouble(0);
                                }
                            } while (reader.NextResult());

                            // 2. Use the AsDataSet extension method
                            DataSet result = reader.AsDataSet();
                            // var result = reader.AsDataSet();
                            
                                foreach (DataRow dr in result.Tables[0].Rows.Cast<DataRow>().Skip(1))
                                {

                                conn.Insert(new DbManager()
                                    {
                                        Glucose = float.Parse(dr[0].ToString()),
                                        Reading = dr[1].ToString(),
                                        Reading1 = dr[2].ToString(),
                                        Date = dr[3].ToString(),
                                        Time = dr[4].ToString(),
                                        Value = float.Parse(dr[5].ToString()),
                                        Date1 = Convert.ToDateTime(dr[9].ToString()),
                                        Comments = dr[10].ToString(),
                                    });


                              
                                }
                            
                            // The result of each spreadsheet is in result.Tables
                        }
                    }

                    MessageDialog mydial = new MessageDialog("Data Uploaded Successfully");
                    mydial.Title = "Diabetes Analyzer";
                    await mydial.ShowAsync();
                }
                catch (Exception)
                {
                    MessageDialog mydial = new MessageDialog("Error\n Please Upload Correct Excel File.");
                    mydial.Title = "Diabetes Analyzer";
                    await mydial.ShowAsync();
                }
                // Application now has read/write access to the picked file
                //  OutputTextBlock.Text = "Picked photo: " + file.Name;
            }

            else
            {
                //OutputTextBlock.Text = "Operation cancelled.";
            }
        }
    }
}
