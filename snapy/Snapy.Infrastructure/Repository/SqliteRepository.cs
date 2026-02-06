using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.Sqlite;

public class SqliteRepository
{
    public SqliteRepository()
    {
    }
    public void SetUpDatabase()
    {
        try
        {

            CreateTable();


        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
        }
        sqliteConnection.Close();


    }
    public void CreateTable()
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {

                    md.CommandText = "CREATE TABLE IF NOT EXISTS ImageText (Id INT PRIMARY KEY  ,Path Text, Text TEXT)";
                    md.ExecuteNonQuery();
                }
            }

        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());

        }

    }
    public void InsertData(string path, string text)
    {
        try
        {

            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {

                    md.CommandText = "INSERT INTO ImageText (Path,Text) VALUES ($path,$text)";
                    md.Parameters.AddWithValue("$text", text);
                    md.Parameters.AddWithValue("$path", path);
                    md.ExecuteNonQuery();
                }
            }

        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());

        }
    }
    public static void SearchTextFromDBS(string searchText)
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = @"SELECT DISTINCT Path FROM ImageText WHERE Text Like $searchText";
                    md.Parameters.AddWithValue("$searchText", "%" + searchText + "%");
                    SqliteDataReader r = md.ExecuteReader();
                    int flag = 0;
                    while (r.Read())
                    {
                        string path = r.GetString(0);
                        Console.WriteLine("Path : " + path);
                        Console.WriteLine("------");
                        flag = 1;

                    }
                    if (flag == 0) Console.WriteLine("Not Found");
                    conn.Close();
                }

            }


        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());

        }
    }


    public bool IsImageProcessed(string path)
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = "SELECT Path FROM ImageText WHERE path=$path";
                    md.Parameters.AddWithValue("$path", path);
                    SqliteDataReader r = md.ExecuteReader();
                    if (r.HasRows)
                    {
                        return true;
                    }
                    return false;

                }


            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
            return false;
        }

    }
    public void CleanImageTextInDB()
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = "DELETE FROM ImageText";
                    md.ExecuteNonQuery();

                }


            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
        }

    }

    public string GetImageText(string path)
    {
        try
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=textFiles.db"))
            {
                conn.Open();
                using (SqliteCommand md = conn.CreateCommand())
                {
                    md.CommandText = "SELECT DISTINCT Text FROM ImageText WHERE Path=$path";
                    md.Parameters.AddWithValue("$path", path);
                    using var r = md.ExecuteReader();
                    if (!r.Read()) return null;
                    if (r.IsDBNull(0)) return null; // if the text is null
                    string text = r.GetString(0);

                    return text;

                }


            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine("Exception in sqlite " + ex.GetBaseException());
            return null;
        }

    }

}