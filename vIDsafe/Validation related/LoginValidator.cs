using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public class LoginValidator
    {
        /// <summary>
        /// Checks if the name and password are valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        public static bool IsValid(string name, string password)
        {
            if (ValidateName(name))
            {
                if (ValidatePassword(password))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the name is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter a name");
                return false;
            }
            else
            {
                if (name.Length < 8)
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Name is lower than 8 characters");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the password is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter a password");
                return false;
            }

            return true;
        }
    }
}
