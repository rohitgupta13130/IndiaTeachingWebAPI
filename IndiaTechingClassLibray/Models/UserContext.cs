using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class UserContext
    {
        private static readonly Lazy<UserContext> _instance =
    new Lazy<UserContext>(() => new UserContext());

        public static UserContext Instance => _instance.Value;

        public Users _user { get; private set; }

        private UserContext() { }

        public void SetUser(Users  argUser)
        {
            _user = argUser;
        }

        public void ClearUser()
        {
            _user = null;
        }
    }
}