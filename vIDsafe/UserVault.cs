using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    public class UserVault
    {
        private List<Identity> _identities = new List<Identity>();

        public UserVault()
        {

        }

        public void NewIdentity(string name)
        {
            Identity identity = new Identity(name);
            _identities.Add(identity);

            vIDsafe.Main.User.SaveVault();
        }

        public Identity GetIdentity(int index)
        {
            return _identities[index];
        }

        public List<Identity> Identities => _identities;

        public void DeleteIdentity(int index)
        {
            _identities.RemoveAt(index);
            vIDsafe.Main.User.SaveVault();
        }
    }
}
