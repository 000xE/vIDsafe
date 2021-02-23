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
        /// Checks if the password is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidatePassword(string password)
        {
            if (isEmpty(password))
            {
                ShowError("Validation error", "Please enter a password");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the name is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private static bool ValidateName(string name)
        {
            if (isEmpty(name))
            {
                ShowError("Validation error", "Please enter a name");
                return false;
            }
            else
            {
                if (name.Length < 8)
                {
                    ShowError("Validation error", "Name is lower than 8 characters");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the passwords are valid and match
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        protected static bool ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (!ValidatePassword(password))
            {
                return false;
            }
            else if (isEmpty(confirmPassword))
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