using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using OPT_2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OPT_2021.Migration
{
    public class FireBaeLV
    {
        private const string NameTableUser = "User/";
        private const string NameTableTokenValid = "Token/";

        private static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "nLRlRpxkbExRPYpo8ejOKxAUmp92kCViQYXhrrJU",
            BasePath = "https://leverus-9117e-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        private static IFirebaseClient client;
        public FireBaeLV() {
            client = new FireSharp.FirebaseClient(config);
        }

        /// <summary>
        /// crea un usuario en firebase
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="banned"></param>
        public async void CreateUser(string email, int role, string name, int banned)
        {
            try
            {
                var data = new User
                {
                    EMAIL = email,
                    ROLE = role,
                    NAME = name,
                    BANNED = banned,
                    TOKEN = null
                };
                SetResponse response = await client.SetAsync(
                    NameTableUser + email, data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// crea un token valido
        /// </summary>
        /// <param name="token"></param>
        /// <param name="VALIDTOKEN"></param>
        /// <param name="IPACESS"></param>
        /// <param name="PATHACESS"></param>
        public async void CreateToken(string token, DateTime VALIDTOKEN, string PATHACESS)
        {
            try
            {               
                var data = new Token
                {
                   VALIDTOKEN = VALIDTOKEN,
                   PATHACESS = PATHACESS
                };
                SetResponse response = await client.SetAsync(
                    NameTableTokenValid + token, data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Actualiza un usuario en firebase
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="banned"></param>
        public async void UpdateUser(string email, int role, string name, int banned, string token = null)
        {
            try
            {
                var data = new User
                {
                    EMAIL = email,
                    ROLE = role,
                    NAME = name,
                    BANNED = banned,
                    TOKEN = token
                };
                FirebaseResponse response = await client.UpdateAsync(
                    NameTableUser + email, data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Lee los datos del usuario
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User ReadUser(string email) {
            try
            {
                FirebaseResponse response = client.Get(NameTableUser + email);
                User obj = response.ResultAs<User>();
                return obj;
            }
            catch (Exception)
            {
                return null;
                throw;

            }
        }

        /// <summary>
        /// lee token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Token ReadToken(string token)
        {
            try
            {
                FirebaseResponse response = client.Get(NameTableTokenValid + token);
                Token obj = response.ResultAs<Token>();
                return obj;
            }
            catch
            {
                var data = new Token
                {
                    VALIDTOKEN = DateTime.Now.AddDays(-100),
                    PATHACESS = String.Empty
                };
                return data;
            }
        }

        /// <summary>
        /// cancela token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CancelToken(string token)
        {
            try
            {
                FirebaseResponse response = client.Delete(NameTableTokenValid + token);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
