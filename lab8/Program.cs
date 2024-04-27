using System;
using System.IO;
using Microsoft.Data.Sqlite;
using SQL;

public class Program
{
    public static void Main(string[] args)
    {   
        string filePath = "input.csv";
        char separator = ',';
        string connectionString = "Data Source=database/db.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var database = new Database();
            var (data, header) = database.ReadFile(filePath, separator);
            var (columnTypes, allowNull) = database.AnalyzeData((data, header));

            string tableName = "YourTableName";
            database.CreateTable((columnTypes, allowNull, header), tableName, connection);
            database.InsertData(data, header, tableName, connection);

            database.PrintTable(tableName, connection);
        } 
    }
}