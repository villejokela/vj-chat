using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VJchat
{
    interface IDatabase
    {
        void CreateTable();
        void InsertChatLine(string name, string text);
        void GetAllEntities();
    }
}
