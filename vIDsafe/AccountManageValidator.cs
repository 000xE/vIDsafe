using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    class AccountManageValidator : AccountValidator
    {
        /// <summary>
        /// Checks if the passwords are valid and match
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        public static new bool IsValid(string password, string confirmPassword)
        {
            if (ValidateConfirmPassword(password, confirmPassword))
            {
                return true;
            }

            return false;
        }
    }
}
