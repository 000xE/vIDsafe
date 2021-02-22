using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    public class IdentityValidator : DetailValidator
    {
        /// <summary>
        /// Checks if the name and email are valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        public static bool IsValid(string name, string email)
        {
            if (ValidateName(name))
            {
                if (ValidateEmail(email))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the email is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ShowError("Validation error", "Please enter a email");

                return false;
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(email);

                    return true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);

                    ShowError("Validation error", "Invalid email format");

                    return false;
                }
            }
        }
    }
}
