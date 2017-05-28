using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SimpleInjector;

namespace VJchat
{
    public class ChatHub : Hub, IHub
    {
        public class UserClass
        {
            public UserClass(string name, string connectionID)
            {
                this.userName = name;
                this.connectionID = connectionID;
            }

            public UserClass() { }

            public string userName;
            public string connectionID;
        }

        static List<UserClass> userList = new List<UserClass>();
        static string userName;
        
  
        //Database db = new Database();

        public void Send(string name, string message)
        {
            var db = StartUp.container.GetInstance<Database>();
            // Save msg to DB
            db.InsertChatLine(name, message);
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void AddUser(string name)
        {
            userName = name;
            string connectionID = Context.ConnectionId;

            userList.Add(new UserClass(userName, connectionID));
            Clients.All.addOnlineUsers(userList);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Add your own code here.
            // For example: in a chat application, mark the user as offline, 
            // delete the association between the current connection id and user name.
            //userList.Remove(userName

            string connectionID = Context.ConnectionId;
            UserClass userToRemove = userList.Find(x => x.connectionID == connectionID);

            userList.Remove(userToRemove);
            Clients.All.addOnlineUsers(userList);

            return base.OnDisconnected(stopCalled);
        }


    }
}