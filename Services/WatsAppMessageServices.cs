using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi;

namespace Beruwala_Mirror.Services
{
    public class WatsAppMessageServices : IWatsAppMessageServices
    {
        public void SendMessage(string message, string toNumber)
        {
            string from = "447885860529";
            string to = "447474318713";
            string password;
           // var res = WhatsAppApi.Register.WhatsRegisterV2.GetToken(from);

            WhatsApp wa = new WhatsApp(from, "", "BeruwalaMirror",false,false);
            wa.OnConnectSuccess += () =>
            {
                Console.Write("Connected");
                wa.OnLoginSuccess += (phoneNumber, data) =>
                {
                    wa.SendMessage(to, message);
                    Console.Write("Message sent ...");
                };
                wa.OnLoginFailed += (data) => { Console.Write("login failed: {0}", data); };
                wa.Login();
            };
            wa.OnConnectFailed += (ex) => { Console.Write("connection failed.."); };
            wa.Connect();
        }

    }
}
