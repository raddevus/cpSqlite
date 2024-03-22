namespace sqliteThreads.Model;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

public class ThreadDataContext : DbContext
{
    // The variable name must match the name of the table.
    public DbSet<ThreadData> ThreadData { get; set; }
    
    public string DbPath { get; }

    public ThreadDataContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "thread.db");
        Console.WriteLine(DbPath);

        SqliteConnection connection = new SqliteConnection($"Data Source={DbPath}");
        // ########### FYI THE DB is created when it is OPENED ########
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        FileInfo fi = new FileInfo(DbPath);
        if (fi.Length == 0){
            try{
                foreach (String tableCreate in allTableCreation){
                    command.CommandText = tableCreate;
                    command.ExecuteNonQuery();
                }
            }
            catch{} // insuring that multiple threads don't try to create table
        }
    }

   // configures the database for use by EF
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    protected String [] allTableCreation = {
        @"CREATE TABLE ThreadData
            (
            [ID] INTEGER NOT NULL PRIMARY KEY,
            [ThreadId] NVARCHAR(30) NOT NULL check(length(ThreadId) <= 30),
            [Created] NVARCHAR(30) default (datetime('now','localtime')) check(length(Created) <= 30)
            )"
    };

}
