using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    class Identity
    {
        private List<Credential> credentials = new List<Credential>();

        public Identity()
        {

        }
    }
}
