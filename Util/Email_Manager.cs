using OPT_2021.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace OPT_2021.Util
{
    public class Email_Manager
    {
        private const string SERVER_EMAIL = "imap.ionos.es";
        private const string NAME_EMAIL = "@leverus.es";
        private const int PORT_EMAIL = 993;
        public static bool ComprobatorEmailValid(string email, string pass)
        {
            try
            {
                ImapX.ImapClient client = null;
                client = new ImapX.ImapClient(SERVER_EMAIL, PORT_EMAIL, SslProtocols.Tls, true);
                if (client.Connect())
                {
                    if (client.Login(email + NAME_EMAIL, pass))
                    {
                        // login successful
                        return true;
                    }
                    // login failed
                    return false;
                }
                else
                {
                    // connection not successful
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
