using System;
using System.Security.Principal;

namespace Comments.Models
{
    public class User : IIdentity, IPrincipal
    {
        readonly string _username;

        public User(string username) {
            _username = username;
        }

        public string Name {
            get { return _username; }
        }

        public string AuthenticationType {
            get { return "temp"; }
        }

        public bool IsAuthenticated {
            get { return true; }
        }

        public bool IsInRole(string role) {
            throw new NotImplementedException();
        }

        public IIdentity Identity {
            get { return this; }
        }
    }
}