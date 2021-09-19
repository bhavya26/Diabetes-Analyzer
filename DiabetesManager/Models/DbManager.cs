
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesManager.Models
{
    public class DbManager
    {
        public float Glucose { get; set; }
        public string Reading { get; set; }
        public string Reading1 { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public float Value { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Avg { get; set; }
        public DateTime Date1 { get; set; }
        public string Comments { get; set; }
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
    }

    public class DataManager
    {

        public static List<DbManager> GetRecord()
       
        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderByDescending(p => p.id).ToList();

            

            return database;
        }
    }
    public class BreakfastData
    {
        public static List<DbManager> GetRecord1()
        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderByDescending(p => p.id).Where(q => q.Reading == "Breakfast").ToList();


            return database;
        }
    
    }
    public class LunchData
    {
        public static List<DbManager> GetRecord2()
        {
            string path;
           SQLite.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderByDescending(p => p.id).Where(q => q.Reading == "Lunch").ToList();


            //path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            //conn = new SQLite.SQLiteConnection(path);

            return database;
        }

    }
    public class DinnerData
    {
        public static List<DbManager> GetRecord3()
        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderByDescending(p => p.id).Where(q => q.Reading == "Dinner").ToList();
           
           
            return database;
        }

    }
    public class DataManager1
    {

        public static List<DbManager> GetRecord()

        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderByDescending(p => p.id).ToList();

            
            return database;
        }
    }
    public class GraphData
    {
        public static List<DbManager> GetRecord()

        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderBy(p => p.Date1).ToList();

           
            return database;
        }
    }
    public class GraphData1
    {
        public static List<DbManager> GetRecord1()
        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderBy(p => p.Date1).Where(q => q.Reading == "Breakfast").ToList();
            
            return database;
        }

    }
    public class GraphData2
    {
        public static List<DbManager> GetRecord2()
        {
            string path;
            SQLite.SQLiteConnection conn;
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderBy(p => p.Date1).Where(q => q.Reading == "Lunch").ToList();
           
            return database;
        }

    }
    public class GraphData3
    {
        public static List<DbManager> GetRecord3()
        {
            string path;
            SQLite.SQLiteConnection conn;
           
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.SQLiteConnection(path);
            var database = conn.Table<DbManager>().OrderBy(p => p.Date1).Where(q => q.Reading == "Dinner").ToList();
          
            return database;
        }

    }
}
