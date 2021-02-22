namespace vIDsafe
{
    public class AccountValidator : DetailValidator
    {
        /// <summary>
        /// Checks if the name and passwords are valid and mtch
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        public static bool IsValid(string name, string password, string confirmPassword)
        {
            if (ValidateName(name))
            {
                if (ValidateConfirmPassword(password, confirmPassword))
                {
                    return true;
                }
            }

            return false;
        }

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
        /// Checks if the passwords are valid and match
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        public static bool ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (!ValidatePassword(password))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(confirmPassword))
            {
                ShowError("Validation error", "Please confirm the password");
                return false;
            }
            else
            {
                if (!password.Equals(confirmPassword))
                {
                    ShowError("Validation error", "Passwords are not the same");
                    return false;
                }
            }

            return true;
        }
    }
}