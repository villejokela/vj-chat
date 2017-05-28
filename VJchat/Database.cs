using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;

namespace VJchat
{
    public class Database : IDatabase
    {
        const string TABLENAME = "VJChatEntries";
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        CloudTableClient tableClient;
        CloudTable chatTable;

        public class ChatLine : TableEntity
        {
            public ChatLine(string name, string text)
            {
                this.PartitionKey = name;
                this.RowKey = text;
            }

            public ChatLine() { }
        }

        public void CreateTable()
        {
            tableClient = storageAccount.CreateCloudTableClient();
            chatTable = tableClient.GetTableReference(TABLENAME);
            chatTable.CreateIfNotExists();
        }

        public void InsertChatLine(string name, string text)
        {
            try
            {
                tableClient = storageAccount.CreateCloudTableClient();
                chatTable = tableClient.GetTableReference(TABLENAME);
                // Create a chatline entity.
                ChatLine chatline = new ChatLine(name, text);

                // Create the TableOperation object that inserts the´chat msg entity.
                TableOperation insertOperation = TableOperation.Insert(chatline);

                // Execute the insert operation.
                chatTable.Execute(insertOperation);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }

        }

        public void GetAllEntities()
        {
            tableClient = storageAccount.CreateCloudTableClient();
            chatTable = tableClient.GetTableReference(TABLENAME);

            var entities = chatTable.ExecuteQuery(new TableQuery<ChatLine>()).ToList();

        }
    }
}