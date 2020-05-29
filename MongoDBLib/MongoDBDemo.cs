using System;
using MongoDB.Driver;

namespace MongoDBLib
{
    public class MongoDBDemo
    {
        public void ListDB()
        {
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017");

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }
    }

}
