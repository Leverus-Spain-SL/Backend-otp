using Microsoft.AspNetCore.Mvc;
using OPT_2021.Migration;
using OPT_2021.Model;
using OPT_2021.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPT_2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        /// <summary>
        /// logear usuario y obtener datos
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ActionResult<User> Login(string email, string pass, string path = null)
        {
            if (Email_Manager.ComprobatorEmailValid(email, pass))
            {
                FireBaeLV test = new FireBaeLV();
                if (Convert.ToInt32(test.ReadUser(email).BANNED) == Convert.ToInt32(BannedUser.BANNED)) return null;
                var user_acces = test.ReadUser(email);

                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string token = Convert.ToBase64String(time.Concat(key).ToArray());
                user_acces.TOKEN = token.Replace("/", "0").Replace("+","o");
                test.UpdateUser(user_acces.EMAIL, user_acces.ROLE, user_acces.NAME, user_acces.BANNED, user_acces.TOKEN);
                test.CreateToken(user_acces.TOKEN,
                    DateTime.Now.AddMinutes(30),
                    path);
                return user_acces;
            }
            return null;
        }

        /// <summary>
        /// crear usuario, si no añades rol se incorporará el rol más pequeño
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="banned"></param>
        /// <returns></returns>
        [HttpPut("createuser")]
        public ActionResult<string> CreateUser(string email, string pass, string name, int role = 6, int banned = 0)
        {
            if (Email_Manager.ComprobatorEmailValid(email, pass))
            {
                FireBaeLV test = new FireBaeLV();

                test.CreateUser(email, role, name, banned);
               // if (Convert.ToInt32(test.ReadUser(email).ROLE) != Convert.ToInt32(RoleUser.ADMIN)) return "Bad rol acces";
                return String.Format("User {0} created with {1} and role {2}. Status ban: {3}",
                    name,
                    email,
                    role,
                    banned);
            }
            return "Bad credential or error in server for create user.";
        }

        /// <summary>
        /// actualiza usuario
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="banned"></param>
        /// <returns></returns>
        [HttpPut("updateuser")]
        public ActionResult<string> UpdateUser(string email, string pass, int role, string name, int banned)
        {
            if (Email_Manager.ComprobatorEmailValid(email, pass))
            {
                FireBaeLV test = new FireBaeLV();
                if (Convert.ToInt32(test.ReadUser(email).ROLE) != Convert.ToInt32(RoleUser.ADMIN)) return "Bad rol acces";
                test.UpdateUser(email, role, name, banned);
                return String.Format("User {0} updated with {1} and role {2}. Status ban: {3}",
                    name,
                    email,
                    role,
                    banned);
            }
            return "Bad credential or error in server for update user.";
        }


        [HttpGet("oauth")]
        public ActionResult<Token> isValidToken(string u)
        {

            try
            {
                FireBaeLV test = new FireBaeLV();
                return test.ReadToken(u);
            }
            catch
            {
                return null;

            }
        }

        [HttpPost("oauth-out")]
        public ActionResult<bool> CancelToken(string u)
        {
            try
            {
                FireBaeLV test = new FireBaeLV();
                if (test.CancelToken(u)) return true;
                return false;
            }
            catch
            {
                return null;

            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
