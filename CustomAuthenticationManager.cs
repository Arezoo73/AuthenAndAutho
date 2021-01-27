using AuthenAndAutho.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenAndAutho
{
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly IList<UserCred> users = new List<UserCred>
        { new UserCred { UserName= "arezoo", Password= "1234", Role= "User"  },
          new UserCred { UserName= "test2", Password= "password2", Role="Administrator" }
        };

        private readonly IDictionary<string, Tuple<string, string>> tokens =
           new Dictionary<string, Tuple<string, string>>();
        public IDictionary<string, Tuple<string, string>> Tokens => tokens;

        public string Authenticate(string UserName, string Password)
        {

            if (!users.Any(u => u.UserName == UserName && u.Password == Password))
            {
                return null;
            }
            var token = Guid.NewGuid().ToString();
            tokens.Add(token, new Tuple<string, string>(UserName,
                users.First(u => u.UserName == UserName && u.Password == Password).Role));
            return token;
        }
    }
}
