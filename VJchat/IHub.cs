using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VJchat
{
    interface IHub
    {
        void Send(string name, string message);
        void AddUser(string name);
        Task OnDisconnected(bool stopCalled);
    }
}
