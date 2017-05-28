using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using SimpleInjector;

[assembly: OwinStartup(typeof(VJchat.StartUp))]

namespace VJchat
{
    public class StartUp
    {
        static public Container container;

        public void Configuration(IAppBuilder app)
        {
            container = new Container();
            container.Register<IHub, ChatHub>(Lifestyle.Singleton);
            container.Register<IDatabase, Database>();

            container.Verify();

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();

            // Create DB for msgs

            var handler = container.GetInstance<Database>();
            handler.CreateTable();

            //Database db = new Database();
            //db.CreateTable();
        }
    }
}
