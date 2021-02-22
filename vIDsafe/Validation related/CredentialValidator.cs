using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public class CredentialValidator
    {
        /// <summary>
        /// Checks if the URL, username and password are valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        public static bool IsValid(string URL, string username, string password)
        {
            if (ValidateURL(URL))
            {
                if (ValidateUsername(username))
                {
                    if (ValidatePassword(password))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the URL is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidateURL(string URL)
        {
            if (string.IsNullOrEmpty(URL))
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter a URL");
                return false;
            }
            else
            {
                bool result = Uri.TryCreate(URL, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme.Equals(Uri.UriSchemeHttp) || uriResult.Scheme.Equals(Uri.UriSchemeHttps));

                if (result)
                {
                    return true;
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Invalid URL format");
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks if the username is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter a username");
                return false;
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
